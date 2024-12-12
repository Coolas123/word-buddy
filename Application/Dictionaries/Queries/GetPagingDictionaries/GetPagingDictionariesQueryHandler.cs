using Application.Abstractions.Messaging;
using Application.Dictionaries.Queries.GetDictionaries;
using Domain.Entities;
using Domain.Shared;
using Domain.TagHelperModels;
using MediatR;

namespace Application.Dictionaries.Queries.GetPagingDictionaries
{
    internal class GetPagingDictionariesQueryHandler : IQueryHandler<GetPagingDictionariesQuery,(PagingInfo, IEnumerable<Dictionary>)>
    {
        private readonly ISender sender;

        public GetPagingDictionariesQueryHandler(ISender sender)
        {
            this.sender = sender;
        }
        public async Task<Result<(PagingInfo, IEnumerable<Dictionary>)>> Handle(GetPagingDictionariesQuery request, CancellationToken cancellationToken)
        {
            var allDictionariesResult = await sender.Send(new GetDictionariesQuery { UserId=request.UserId });

            if (allDictionariesResult.IsSuccess) {
                var dictionaries = 
                    allDictionariesResult
                    .Value()
                    .OrderByDescending(x=>x.LastViewedAt)
                    .Skip((request.PagingInfo.CurrentPage - 1)
                        * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);

                request.PagingInfo.TotalItems= allDictionariesResult.Value().Count();

                return Result.Success((request.PagingInfo, dictionaries));

            }
            else {
                return Result.Failure<(PagingInfo, IEnumerable<Dictionary>)>(allDictionariesResult.Error);
            }
        }
    }
}
