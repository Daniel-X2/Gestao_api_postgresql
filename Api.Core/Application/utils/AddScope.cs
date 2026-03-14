using Api.core.Application.repository;
using Api.core.Application.service;
using Api.core.Application.utils;


public class AddScope
{
    public void AddScopeFuncion(WebApplicationBuilder builder)
    {
        
        builder.Services.AddScoped<IConnect, ConnectHost>();
        builder.Services.AddScoped<IRepositoryFuncionario,RepositoryFuncionario>();
        builder.Services.AddScoped<IServiceFuncionario,ServiceFuncionario>();
        
        
        builder.Services.AddScoped<IRepositoryClient,RepositoryClient>();
        builder.Services.AddScoped<IServiceCLient, ClientService>();
        
        builder.Services.AddScoped<IServiceProduct, ServiceProduct>();
        builder.Services.AddScoped<IRepositoryProduct, RepositoryProduct>();
    }
}