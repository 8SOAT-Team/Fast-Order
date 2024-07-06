﻿using Postech8SOAT.FastOrder.Domain.Exceptions;

namespace Postech8SOAT.FastOrder.Domain.Entities;
public class Categoria:Entity
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public ICollection<Produto>? Produtos { get; set; }

    public Categoria(string? nome,string? descricao)
    {
        ValidationDomain(nome, descricao);
        
    }
    public Categoria(int id, string? nome, string? descricao)
    {
        DomainExceptionValidation.When(id < 0, "Id inválido");
        Id = id;
        ValidationDomain(nome, descricao);
    }

    public void Update(string? nome, string? descricao)
    {
        ValidationDomain(nome, descricao);
    }

    private void ValidationDomain(string? nome, string? descricao)
    {
       DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome é obrigatório");

        DomainExceptionValidation.When(nome!.Length < 3, "Nome deve ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(nome.Length > 100, "Nome deve ter no máximo 100 caracteres");

        DomainExceptionValidation.When(nome.Length < 3 || nome.Length > 100, "Nome deve ter entre 3 e 100 caracteres");

        DomainExceptionValidation.When(string.IsNullOrEmpty(descricao), "Descrição é obrigatória");

        DomainExceptionValidation.When(descricao!.Length < 3, "Descrição deve ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(descricao.Length > 100, "Descrição deve ter no máximo 100 caracteres");

        DomainExceptionValidation.When(descricao.Length < 3 || descricao.Length > 100, "Descrição deve ter entre 3 e 100 caracteres");

        this.Nome = nome;
        this.Descricao = descricao;
    }
}