using Api.core.Application.repository;
using Api.core.Application.service;
using Api.core.Application.utils;


public class TESTE
{
    public void TEaSTE(WebApplicationBuilder builder)
    {
        
        builder.Services.AddScoped<IConnect, ConnectHost>();
        builder.Services.AddScoped<IRepositoryFuncionario,RepositoryFuncionario>();
        builder.Services.AddScoped<IServiceFuncionario,ServiceFuncionario>();
    }
}