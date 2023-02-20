using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Sakura.Text.Json.JsonFlattener.Core;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.CSharp.SyntaxKind;

namespace JsonFlattener
{
	[Generator]
	internal class JsonFlattenGenerator : ISourceGenerator
	{
		/// <inheritdoc />
		public void Initialize(GeneratorInitializationContext context)
		{
		}

		/// <inheritdoc />
		public void Execute(GeneratorExecutionContext context)
		{
			var flattenAttrSymbol = context.Compilation.TypeSymbol<JsonFlattenAttribute>();
			var jsonIgnoreSymbol = context.Compilation.TypeSymbol<JsonIgnoreAttribute>();

			foreach (var (model, property) in context.Compilation.GetNodes<PropertyDeclarationSyntax>())
			{
				var propertySymbol = model.GetDeclaredSymbol(property)
								  ?? throw new InvalidOperationException(
									  $"Cannot get compiled symbol for property {property.Identifier}.");

				var flattenAttr = propertySymbol.GetAttribute(flattenAttrSymbol);

				// 未处理标记的类型，跳过
				if (flattenAttr == null)
				{
					continue;
				}

				var containingType = property.Parent as TypeDeclarationSyntax
												?? throw new InvalidOperationException(
													$"Cannot get the containing type of property {property.Identifier}");

				if (!containingType.Modifiers.Any(PartialKeyword))
				{
					throw new InvalidOperationException(
						$"{typeof(JsonFlattenAttribute).FullName} requires the containing type {containingType.Identifier} marked as partial type.");
				}

				if (propertySymbol.GetAttribute(jsonIgnoreSymbol) == null)
				{
					throw new InvalidOperationException(
						$"The property {property.Identifier} or type {containingType.Identifier} must be marked with the {typeof(JsonIgnoreAttribute).FullName} attribute.");
				}

				if (propertySymbol.NullableAnnotation == NullableAnnotation.Annotated)
				{
					throw new InvalidOperationException(
						$"The property {property.Identifier} or type {containingType.Identifier} cannot be nullable.");
				}

				var partialType =
					CreatePartialTypeForOneFlattenProperty(containingType, propertySymbol, flattenAttr, model);

				var fileName = $"{containingType.Identifier}.{propertySymbol.Name}.Delegating.cs";
				context.AddSource(fileName, partialType.NormalizeWhitespace().ToFullString());
			}
		}

		private TypeDeclarationSyntax CreatePartialTypeForOneFlattenProperty(TypeDeclarationSyntax type,
			IPropertySymbol flattenProperty, AttributeData data, SemanticModel model)
		{
			var propertyTargetType = flattenProperty.Type;

			var delegatingPropertyList =
				from prop in propertyTargetType.GetMembers().OfType<IPropertySymbol>()
				select CreateDelegatingProperty(flattenProperty, prop, model);

			// partial class ...
			var partialType =
				TypeDeclaration(type.Kind(), type.Identifier)
					.WithModifiers(TokenList(Token(PartialKeyword)))
					.WithMembers(List<MemberDeclarationSyntax>(delegatingPropertyList));

			return partialType;
		}

		private PropertyDeclarationSyntax CreateDelegatingProperty(IPropertySymbol flattenProperty, IPropertySymbol targetProperty, SemanticModel model)
		{
			// this.Flatten.TargetProperty
			var propertyDelegatingExp = ThisExpression().MemberChain(flattenProperty.Name, targetProperty.Name);

			// [JsonIncludeAttribute]
			var jsonIgnoreAttr = Attribute(ParseName(model.Compilation.TypeSymbol<JsonIncludeAttribute>().ToDisplayString()));

			// => this.Flatten.TargetProperty
			var getter =
				AccessorDeclaration(GetAccessorDeclaration)
					.WithExpressionBody(ArrowExpressionClause(propertyDelegatingExp))
					.WithSemicolonToken(Token(SemicolonToken));


			// this.Flatten.TargetProperty = value
			var setter =
				AccessorDeclaration(SetAccessorDeclaration)
					.WithExpressionBody(ArrowExpressionClause(AssignmentExpression(SimpleAssignmentExpression,
						propertyDelegatingExp, IdentifierName("value"))))
					.WithSemicolonToken(Token(SemicolonToken));

			/* [JsonIncludeAttribute]
			 * private TargetType TargetProperty
			 * {
			 *    get => this.Flatten.TargetProperty;
			 *    set => this.Flatten.TargetProperty = value;
			 * }
			 */
			return PropertyDeclaration(ParseTypeName(targetProperty.Type.ToDisplayString()), targetProperty.Name)
				.WithAttributes(jsonIgnoreAttr)
				.WithModifiers(TokenList(Token(PrivateKeyword)))
				.WithAccessors(getter, setter);
		}

	}
}
