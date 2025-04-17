using Ambev.DeveloperEvaluation.Application.Categories.ListCategory;
using Ambev.DeveloperEvaluation.Common.Extensions;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Images.GetImage
{
    public class GetImageHandler : IRequestHandler<GetImageCommand, GetImageResult>
    {
        private readonly INoSqlRepository<BsonDocumentBackedClass> _noSqlRepository;

        public GetImageHandler(INoSqlRepository<BsonDocumentBackedClass> noSqlRepository)
        {
            _noSqlRepository = noSqlRepository;
        }

        public async Task<GetImageResult> Handle(GetImageCommand command, CancellationToken cancellationToken)
        {
            var image = await _noSqlRepository.GridFsBucket.DownloadAsBytesByNameAsync(command.ImageName, cancellationToken: cancellationToken);

            return new GetImageResult
            {
                Success = image != null,
                FileBytes = image,
                ContentType = MimeTypes.GetMimeType(command.ImageName)
            };
        }
    }
}
