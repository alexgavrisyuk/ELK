using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using OrderService.Domain.AggregatesModels.DishAggregate;
using OrderService.Ordering.Models;
using OrderService.Ordering.Queries.DishQueries;

namespace OrderService.Ordering.QueryHandlers
{
    public class DishQueriesHandler :
        IRequestHandler<GetDishByIdQuery, DishResponseModel>,
        IRequestHandler<GetDishesQuery, ICollection<DishResponseModel>>
    {
        private readonly IDishRepository _dishRepository;

        public DishQueriesHandler(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }
        
        public async Task<DishResponseModel> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            var dish = await _dishRepository.GetByIdAsync(request.Id);
            if (dish == null)
            {
                throw new Exception($"Dish with id {request.Id} does not exist");
            }
            
            var response = dish.Adapt<DishResponseModel>();
            return response;
        }

        public async Task<ICollection<DishResponseModel>> Handle(GetDishesQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _dishRepository.GetAsync();

            var response = dishes.Adapt<ICollection<DishResponseModel>>();
            return response;
        }
    }
}