using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Sistema.Controllers;
using Sistema.Data;
using Sistema.Models;
using Sistema.Repositories;
using Sistema.Repositories.Interfaces;
using Xunit.Sdk;

namespace TransactionTests
{
    public class TransactionTests
    {
        [Fact]
        public async Task GetTransactionById()
        {

            var mockRepository = new Mock<ITransactionRepository>();

            var controller = new TransactionController(mockRepository.Object);

            int id = 1;

            mockRepository.Setup(repo => repo.GetTransactionById(id)).ReturnsAsync(new TransactionModel());

            var result = await controller.GetTransactionById(id);

            Assert.IsType<OkObjectResult>(result.Result);

        }

        [Fact]
        public async Task GetAll()
        {

            var mockRepository = new Mock<ITransactionRepository>();

            var controller = new TransactionController(mockRepository.Object);

            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<TransactionModel>());

            var result = await controller.GetAllTransactions();

            Assert.IsType<OkObjectResult>(result.Result);

        }
    }
}