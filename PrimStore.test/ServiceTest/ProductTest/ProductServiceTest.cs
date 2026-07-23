using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using PrimeStore.data.Entities;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.service.Implementations;

namespace PrimStore.test.ServiceTest.ProductTest
{
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _ProductRepositoryMock;
        private readonly Mock<IProductImageRepository> _ProductImageRepositoryMock;
        private readonly Mock<IProductAttributeRepository> _ProductAttributeRepositoryMock;
        private readonly Mock<IDbContextTransaction> _DbContextTransactionMock;

        public ProductServiceTest()
        {
            _ProductRepositoryMock = new();
            _ProductImageRepositoryMock = new();
            _ProductAttributeRepositoryMock = new();
            _DbContextTransactionMock = new();
        }

        [Fact]
        public async Task AddProduct_Product_Success()
        {
            //Arrange
            var moqProduct = new Product { Title = "test", ProductAttributes = { new ProductAttribute { Key = "testkey", Value = "testvalue" } }, ProductImages = { new ProductImage { ProductImageUrl = "link" } } };

            var setup4 = _ProductRepositoryMock.Setup(x => x.BeginTransactionAsync()).Returns(Task.FromResult(_DbContextTransactionMock.Object));
            var setup2 = _ProductImageRepositoryMock.Setup(x => x.AddRangeAsync(It.IsAny<List<ProductImage>>())).Returns(Task.CompletedTask);
            var setup3 = _ProductAttributeRepositoryMock.Setup(x => x.AddRangeAsync(It.IsAny<List<ProductAttribute>>())).Returns(Task.CompletedTask);
            var setup1 = _ProductRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Product>())).Returns(Task.FromResult(moqProduct));

            var services = new ProductService(_ProductRepositoryMock.Object, _ProductImageRepositoryMock.Object, _ProductAttributeRepositoryMock.Object);
            //Act
            var result = await services.AddAsync(moqProduct);

            //Assert
            result.Should().Be("Success");
        }

        //[Fact]
        //public async Task UpdateProduct_Product_Success()
        //{
        //    //Arrange
        //    var moqProduct = new Product { Title = "test", ProductAttributes = { new ProductAttribute { Key = "testkey", Value = "testvalue" } }, ProductImages = { new ProductImage { ProductImageUrl = "link" } } };

        //    var setup4 = _ProductRepositoryMock.Setup(x => x.BeginTransactionAsync()).Returns(Task.FromResult(_DbContextTransactionMock.Object));

        //   // var setup054 = _ProductImageRepositoryMock.Setup(async x =>x.GetTableAsTracking()).Returns(Task.FromResult());
        //    var setup54 = _ProductImageRepositoryMock.Setup(x => x.DeleteRangeAsync(It.IsAny<List<ProductImage>>())).Returns(Task.CompletedTask);
        //    var setup2 = _ProductImageRepositoryMock.Setup(x => x.AddRangeAsync(It.IsAny<List<ProductImage>>())).Returns(Task.CompletedTask);

        //    //var setup0054 = _ProductAttributeRepositoryMock.Setup(x => x.GetTableAsTracking()).Returns(moqProduct.ProductAttributes.AsQueryable());
        //    var setup13 = _ProductAttributeRepositoryMock.Setup(x => x.DeleteRangeAsync(It.IsAny<List<ProductAttribute>>())).Returns(Task.CompletedTask);
        //    var setup3 = _ProductAttributeRepositoryMock.Setup(x => x.AddRangeAsync(It.IsAny<List<ProductAttribute>>())).Returns(Task.CompletedTask);

        //    var setup1 = _ProductRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Product>())).Returns(Task.FromResult(moqProduct));

        //    var services = new ProductService(_ProductRepositoryMock.Object, _ProductImageRepositoryMock.Object, _ProductAttributeRepositoryMock.Object);
        //    //Act
        //    var result = await services.UpdateAsync(moqProduct);

        //    //Assert
        //    result.Should().Be("Success");
        //}

        [Fact]
        public async Task DeleteProduct_Product_Success()
        {
            //Arrange
            var moqProduct = new Product { Title = "test", ProductAttributes = { new ProductAttribute { Key = "testkey", Value = "testvalue" } }, ProductImages = { new ProductImage { ProductImageUrl = "link" } } };

            var setup4 = _ProductRepositoryMock.Setup(x => x.BeginTransactionAsync()).Returns(Task.FromResult(_DbContextTransactionMock.Object));
            var setup1 = _ProductRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            var services = new ProductService(_ProductRepositoryMock.Object, _ProductImageRepositoryMock.Object, _ProductAttributeRepositoryMock.Object);
            //Act
            var result = await services.DeleteAsync(moqProduct);

            //Assert
            result.Should().Be("Success");
        }

    }
}
