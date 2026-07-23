using AutoMapper;
using FluentAssertions;
using Moq;
using PrimeStore.core.Features.Products.Commands.Handlers;
using PrimeStore.core.Features.Products.Commands.Models;
using PrimeStore.core.Mapping.ProductMapping;
using PrimeStore.data.Entities;
using PrimeStore.service.Abstracts;

namespace PrimStore.test.CoreTest.ProductTest.CommandTest
{
    public class ProductHandlerTest
    {
        private readonly Mock<IProductService> _ProductServiceMock;
        private readonly IMapper _MapperMock;

        public ProductHandlerTest()
        {
            _ProductServiceMock = new();
            var configuration = new MapperConfiguration(c => c.AddProfile(new ProductProfile()));
            _MapperMock = new Mapper(configuration);
        }

        [Fact]
        public async Task AddProduct_ProductObject_Success()
        {
            //Arrange
            var handler = new ProductHandler(_ProductServiceMock.Object, _MapperMock);

            var command = new AddProductCommand { Title = "Test" };

            var product = _MapperMock.Map<Product>(command);

            var setup = _ProductServiceMock.Setup(x => x.AddAsync(It.IsAny<Product>())).Returns(Task.FromResult("Success"));

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task AddProduct_ProductObject_BadRequest()
        {
            //Arrange
            var handler = new ProductHandler(_ProductServiceMock.Object, _MapperMock);

            var command = new AddProductCommand { Title = "Test" };

            var product = _MapperMock.Map<Product>(command);

            var setup = _ProductServiceMock.Setup(x => x.AddAsync(It.IsAny<Product>())).Returns(Task.FromResult("BadRequest"));

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateProduct_ProductObjectFound_Success()
        {
            //Arrange
            var handler = new ProductHandler(_ProductServiceMock.Object, _MapperMock);

            var command = new EditProductCommand { ProductId = 1, Title = "Test" };

            var product = _MapperMock.Map<Product>(command);

            var setup2 = _ProductServiceMock.Setup(x => x.GetByIDAsync(1)).Returns(Task.FromResult(product));
            var setup = _ProductServiceMock.Setup(x => x.UpdateAsync(It.IsAny<Product>())).Returns(Task.FromResult("Success"));

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateProduct_ProductObject_BadRequest()
        {
            //Arrange
            var handler = new ProductHandler(_ProductServiceMock.Object, _MapperMock);

            var command = new EditProductCommand { ProductId = 1, Title = "Test" };

            var product = _MapperMock.Map<Product>(command);

            var setup2 = _ProductServiceMock.Setup(x => x.GetByIDAsync(1)).Returns(Task.FromResult(product));
            var setup = _ProductServiceMock.Setup(x => x.UpdateAsync(It.IsAny<Product>())).Returns(Task.FromResult("BadRequest"));

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateProduct_ProductObjectNotFound_NotFound()
        {
            //Arrange
            var handler = new ProductHandler(_ProductServiceMock.Object, _MapperMock);

            var command = new EditProductCommand { ProductId = 1, Title = "Test" };

            var product = _MapperMock.Map<Product>(command);

            var setup2 = _ProductServiceMock.Setup(x => x.GetByIDAsync(1)).ReturnsAsync((Product?)null);
            var setup = _ProductServiceMock.Setup(x => x.UpdateAsync(It.IsAny<Product>())).Returns(Task.FromResult("Success"));

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteProduct_ProductObjectFound_Success()
        {
            //Arrange
            var handler = new ProductHandler(_ProductServiceMock.Object, _MapperMock);

            var ProductDeleted = new Product { ProductId = 1, Title = "Test" };
            var command = new DeleteProductCommand(1);


            var product = _MapperMock.Map<Product>(ProductDeleted);

            var setup2 = _ProductServiceMock.Setup(x => x.GetByIDAsync(1)).Returns(Task.FromResult(product));
            var setup = _ProductServiceMock.Setup(x => x.DeleteAsync(It.IsAny<Product>())).Returns(Task.FromResult("Success"));

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteProduct_ProductObject_BadRequest()
        {
            //Arrange
            var handler = new ProductHandler(_ProductServiceMock.Object, _MapperMock);

            var ProductDeleted = new Product { ProductId = 1, Title = "Test" };
            var command = new DeleteProductCommand(1);


            var product = _MapperMock.Map<Product>(ProductDeleted);

            var setup2 = _ProductServiceMock.Setup(x => x.GetByIDAsync(1)).Returns(Task.FromResult(product));
            var setup = _ProductServiceMock.Setup(x => x.DeleteAsync(It.IsAny<Product>())).Returns(Task.FromResult("BadRequest"));

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteProduct_ProductObjectNotFound_NotFound()
        {
            //Arrange
            var handler = new ProductHandler(_ProductServiceMock.Object, _MapperMock);

            var ProductDeleted = new Product { ProductId = 1, Title = "Test" };
            var command = new DeleteProductCommand(1);


            var product = _MapperMock.Map<Product>(ProductDeleted);

            var setup2 = _ProductServiceMock.Setup(x => x.GetByIDAsync(1)).ReturnsAsync((Product?)null);

            var setup = _ProductServiceMock.Setup(x => x.DeleteAsync(It.IsAny<Product>())).Returns(Task.FromResult("Success"));

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

    }
}
