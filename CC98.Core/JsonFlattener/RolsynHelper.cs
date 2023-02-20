using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace JsonFlattener;

/// <summary>
/// Provide helper methods for Roslyn related code. This class is static.
/// </summary>
public static class RolsynHelper
{
	public static IEnumerable<(SemanticModel Model, T Node)> GetNodes<T>(this Compilation compilation)
	{
		return from tree in compilation.SyntaxTrees
			   let model = compilation.GetSemanticModel(tree)
			   from node in tree.GetRoot().DescendantNodesAndSelf().OfType<T>()
			   select (model, node);
	}

	/// <summary>
	/// Get the type symbol associated with the compilation time type instance.
	/// </summary>
	/// <typeparam name="T">The type provided as the generic type argument.</typeparam>
	/// <param name="compilation">The current Roslyn complication context.</param>
	/// <returns>A <see cref="INamedTypeSymbol"/> associated with the type <typeparamref name="T"/>.</returns>
	/// <exception cref="InvalidOperationException">There is no type with the same <see cref="Type.FullName"/> as the type <typeparamref name="T"/> found in the complication context.</exception>
	public static INamedTypeSymbol TypeSymbol<T>(this Compilation compilation)
	{
		return compilation.GetTypeByMetadataName(typeof(T).FullName)
			   ?? throw new InvalidOperationException(
				   $"The type {typeof(T).FullName} is not introduced in the current assembly.");
	}

	public static AttributeData? GetAttribute(this ISymbol symbol, ITypeSymbol typeSymbol)
	{
		var attrSymbol =
			symbol
				.GetAttributes()
				.SingleOrDefault(
					i => typeSymbol.Equals(i.AttributeClass, SymbolEqualityComparer.IncludeNullability));

		return attrSymbol;
	}

	/// <summary>
	/// Generate a <see cref="Microsoft.CodeAnalysis.SyntaxList{T}"/> with the given items.
	/// </summary>
	/// <typeparam name="T">The element type of the <see cref="Microsoft.CodeAnalysis.SyntaxList{T}"/>.</typeparam>
	/// <param name="items">The items should be added.</param>
	/// <returns>The created <see cref="Microsoft.CodeAnalysis.SyntaxList{T}"/> instance.</returns>
	public static SyntaxList<T> SyntaxList<T>(params T[] items)
		where T : SyntaxNode
		=> SyntaxFactory.List(items);

	/// <summary>
	/// Generate an expression that indicates a simple member access for a parent expression.
	/// </summary>
	/// <param name="expression">The parent expression.</param>
	/// <param name="name">The name of the member to be accessing.</param>
	/// <returns>The generated <see cref="MemberAccessExpressionSyntax"/> instance.</returns>
	public static MemberAccessExpressionSyntax Member(this ExpressionSyntax expression, SimpleNameSyntax name)
	{
		return SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expression, name);
	}

	/// <summary>
	/// Generate an aggregated expression that access a chain of members for a parent expression. 
	/// </summary>
	/// <param name="expression">The parent expression.</param>
	/// <param name="names">The chained member names array to be accessing.</param>
	/// <returns>The generated final <see cref="ExpressionSyntax"/> instance.</returns>
	public static ExpressionSyntax MemberChain(this ExpressionSyntax expression, params string[] names)
	{
		return names.Aggregate(expression, (current, name) => current.Member(SyntaxFactory.IdentifierName(name)));
	}

	public static PropertyDeclarationSyntax WithAccessors(this PropertyDeclarationSyntax syntax, params AccessorDeclarationSyntax[] accessors) => syntax.WithAccessorList(SyntaxFactory.AccessorList(SyntaxFactory.List(accessors)));

	public static PropertyDeclarationSyntax WithAttributes(this PropertyDeclarationSyntax syntax,
		params AttributeSyntax[] attributes)
		=> syntax.WithAttributeLists(SyntaxList(SyntaxFactory.AttributeList(SyntaxFactory.AttributeTargetSpecifier(SyntaxFactory.Token(SyntaxKind.PropertyKeyword)),
			SyntaxFactory.SeparatedList(attributes))));
}