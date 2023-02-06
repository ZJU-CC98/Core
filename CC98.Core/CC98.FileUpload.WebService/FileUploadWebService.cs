using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CC98.Services.Web;

public class FileUploadWebService
{
	private FileUploadService InnerService { get; }


	public FileUploadWebService(IOptions<FileUploadServiceConfig> configOptions)
	{
		InnerService = new(configOptions.Value);
	}

	/// <summary>
	/// 将使用 HTTP 上传的文件上传到服务器。使用压缩设置。
	/// </summary>
	/// <param name="files">要上传的一个或多个表单文件。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。任务结果包含上传后实际文件的访问地址。</returns>
	public Task<IEnumerable<string>> UploadAsync([NotNull] IEnumerable<IFormFile> files,
		CancellationToken cancellationToken = default)
		=> UploadAsync(files, InnerService.Config.DefaultSubPath, InnerService.Config.CompressByDefault, cancellationToken);

	/// <summary>
	/// 将使用 HTTP 上传的文件上传到服务器。
	/// </summary>
	/// <param name="files">要上传的一个或多个表单文件。</param>
	/// <param name="compressImage">是否压缩图像。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。任务结果包含上传后实际文件的访问地址。</returns>
	public async Task<IEnumerable<string>> UploadAsync([NotNull] IEnumerable<IFormFile> files, string subPath, bool compressImage,
		CancellationToken cancellationToken = default)
	{
		if (files == null)
		{
			throw new ArgumentNullException(nameof(files));
		}


		// 没有任何文件
		if (!files.Any())
		{
			return Array.Empty<string>();
		}

		var realFiles = files.Select(i => new UploadFileInfo
		{
			FileName = i.FileName,
			Stream = i.OpenReadStream()
		});

		try
		{
			return await InnerService.UploadAsync(realFiles, subPath, compressImage, cancellationToken);
		}
		finally
		{
			// 关闭数据流
			foreach (var i in realFiles)
			{
				i.Stream.Close();
			}
		}
	}

	/// <summary>
	/// 将本地文件上传到服务器。使用默认设置。
	/// </summary>
	/// <param name="filePaths">要上传的一个或多个本地文件的路径。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。任务结果包含上传后实际文件的访问地址。</returns>
	public Task<IEnumerable<string>> UploadAsync(IEnumerable<string> filePaths,
		CancellationToken cancellationToken = default)
		=> UploadAsync(filePaths, InnerService.Config.DefaultSubPath, InnerService.Config.CompressByDefault, cancellationToken);

	/// <summary>
	/// 将本地文件上传到服务器。
	/// </summary>
	/// <param name="filePaths">要上传的一个或多个本地文件的路径。</param>
	/// <param name="subPath">上传子路径。</param>
	/// <param name="compressImage">是否压缩图像。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。任务结果包含上传后实际文件的访问地址。</returns>
	public async Task<IEnumerable<string>> UploadAsync(IEnumerable<string> filePaths, string subPath, bool compressImage, CancellationToken cancellationToken = default)
	{
		if (filePaths == null)
		{
			throw new ArgumentNullException(nameof(filePaths));
		}


		// 没有任何文件
		if (!filePaths.Any())
		{
			return Array.Empty<string>();
		}

		var realFiles = filePaths.Select(i => new UploadFileInfo
		{
			FileName = Path.GetFileName(i),
			Stream = File.OpenRead(i)
		});

		try
		{
			return await InnerService.UploadAsync(realFiles, subPath, compressImage, cancellationToken);
		}
		finally
		{
			// 关闭数据流
			foreach (var i in realFiles)
			{
				i.Stream.Close();
			}
		}
	}

	/// <summary>
	/// 将给定的文件上传到服务器。使用默认设置。
	/// </summary>
	/// <param name="files">要上传的一个或多个文件的信息。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。任务结果包含上传后实际文件的访问地址。</returns>
	public Task<IEnumerable<string>> UploadAsync(IEnumerable<UploadFileInfo> files,
		CancellationToken cancellationToken = default)
		=> UploadAsync(files, InnerService.Config.DefaultSubPath, InnerService.Config.CompressByDefault, cancellationToken);

	/// <summary>
	/// 将给定的文件上传到服务器。
	/// </summary>
	/// <param name="files">要上传的一个或多个文件的信息。</param>
	/// <param name="subPath">子路径。</param>
	/// <param name="compressImage">是否压缩图像。</param>
	/// <param name="cancellationToken">用于取消操作的令牌。</param>
	/// <returns>表示异步操作的任务。任务结果包含上传后实际文件的访问地址。</returns>
	public async Task<IEnumerable<string>> UploadAsync(IEnumerable<UploadFileInfo> files, string subPath, bool compressImage, CancellationToken cancellationToken = default)
	{
		if (files == null)
		{
			throw new ArgumentNullException(nameof(files));
		}


		// 没有任何文件
		if (!files.Any())
		{
			return Array.Empty<string>();
		}

		return await InnerService.UploadAsync(files, subPath, compressImage, cancellationToken);
	}

}