using AutoMapper;
using Moq;
using PrimeStore.core.Features.Products.Queries.Models;
using PrimeStore.core.Features.Products.Queries.Results;
using PrimeStore.core.Mapping.ProductMapping;
using PrimeStore.data.Entities;
using PrimeStore.service.Abstracts;

namespace PrimStore.test.CoreTest.ProductTest.QueryTest
{
    public class ProductHandlerTest
    {
        private readonly Mock<IProductService> _ProductServiceMock;
        private readonly Mock<ICategoryService> _CategoryServiceMock;
        private readonly IMapper _MapperMock;

        public ProductHandlerTest()
        {
            _ProductServiceMock = new();
            _CategoryServiceMock = new();
            var configuration = new MapperConfiguration(c => c.AddProfile(new ProductProfile()));
            _MapperMock = new Mapper(configuration);
        }


        [Fact]
        public async Task GetProductList_GetAll_NotNull()
        {
            //Arrange

            var productList = new List<Product> { new Product { Title = "Test" }, new Product { Title = "Test2" } };

            var handler = new PrimeStore.core.Features.Products.Queries.Handlers.ProductHandler(_ProductServiceMock.Object, _CategoryServiceMock.Object, _MapperMock);

            var command = new GetProductListQuery();

            var setup = _ProductServiceMock.Setup(x => x.GetProductsListAsync()).Returns(Task.FromResult(productList));

            var productListMapper = _MapperMock.Map<List<GetProductListResponse>>(productList);
            //Act

            var result = await handler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
        }
        public async Task GetProductById_GetAll_NotNull()
        {
            //Arrange

            var product = new Product { Title = "Test" };

            var handler = new PrimeStore.core.Features.Products.Queries.Handlers.ProductHandler(_ProductServiceMock.Object, _CategoryServiceMock.Object, _MapperMock);

            var command = new GetProductListQuery();

            var setup = _ProductServiceMock.Setup(x => x.GetByIDAsync(1)).Returns(Task.FromResult(product));

            var productMapper = _MapperMock.Map<GetProductByIdResponse>(product);

            //Act

            var result = await handler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
        }

    }
}
