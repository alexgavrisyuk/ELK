using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using OrderService.Domain.AggregatesModels.DishAggregate;
using OrderService.Ordering.Commands.DishCommands;
using OrderService.Ordering.Models;

namespace OrderService.Ordering.CommandHandlers
{
    public class DishCommandsHandler :
        IRequestHandler<CreateDishCommand, DishResponseModel>,
        IRequestHandler<UpdateDishCommand, DishResponseModel>,
        IRequestHandler<DeleteDishCommand, DishResponseModel>
    {
        private readonly IDishRepository _dishRepository;

        public DishCommandsHandler(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }
        
        public async Task<DishResponseModel> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            var newDish = new Dish(request.Name, request.Description, request.PhotoPath);
            await _dishRepository.CreateAsync(newDish);

            var response = newDish.Adapt<DishResponseModel>();
            return response;
        }

        public async Task<DishResponseModel> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            var existedDish = await _dishRepository.GetByIdAsync(request.Id);
            if (existedDish == null)
            {
                throw new Exception($"Dish with id {request.Id} does not exist");
            }
            
            existedDish.Update(request.Name, request.Description, request.PhotoPath);
            await _dishRepository.Update(existedDish);

            var response = existedDish.Adapt<DishResponseModel>();
            return response;
        }

        public async Task<DishResponseModel> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            var existedDish = await _dishRepository.GetByIdAsync(request.Id);
            if (existedDish == null)
            {
                throw new Exception($"Dish with id {request.Id} does not exist");
            }

            await _dishRepository.Delete(existedDish);
            
            var response = existedDish.Adapt<DishResponseModel>();
            return response;
        }
    }
}