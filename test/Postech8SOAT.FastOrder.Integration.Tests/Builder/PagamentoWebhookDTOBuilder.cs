using Bogus;
using Postech8SOAT.FastOrder.Upstream.Pagamentos.MercadoPago.Dtos;

namespace Postech8SOAT.FastOrder.Integration.Tests.Builder
{
    internal class PagamentoWebhookDTOBuilder : Faker<PagamentoWebhookDTO>
    {
        public PagamentoWebhookDTOBuilder()
        {
            CustomInstantiator(f => new PagamentoWebhookDTO
            {
                Id = f.Random.Long(1, 1000),
                Action = f.Lorem.Word(),
                ApiVersion = f.Random.AlphaNumeric(5),
                Data = new PagamentoWebhookDataDTO
                {
                    Id = f.Random.AlphaNumeric(10)
                },
                DateCreated = f.Date.Past(),
                LiveMode = f.Random.Bool(),
                Type = f.Lorem.Word(),
                UserId = f.Internet.UserName()
            });
        }

        public PagamentoWebhookDTOBuilder ComId(long id)
        {
            CustomInstantiator(f => new PagamentoWebhookDTO
            {
                Id = id,
                Action = f.Lorem.Word(),
                ApiVersion = f.Random.AlphaNumeric(5),
                Data = new PagamentoWebhookDataDTO
                {
                    Id = f.Random.AlphaNumeric(10)
                },
                DateCreated = f.Date.Past(),
                LiveMode = f.Random.Bool(),
                Type = f.Lorem.Word(),
                UserId = f.Internet.UserName()
            });
            return this;
        }

        public PagamentoWebhookDTOBuilder ComAction(string action)
        {
            CustomInstantiator(f => new PagamentoWebhookDTO
            {
                Id = f.Random.Long(1, 1000),
                Action = action,
                ApiVersion = f.Random.AlphaNumeric(5),
                Data = new PagamentoWebhookDataDTO
                {
                    Id = f.Random.AlphaNumeric(10)
                },
                DateCreated = f.Date.Past(),
                LiveMode = f.Random.Bool(),
                Type = f.Lorem.Word(),
                UserId = f.Internet.UserName()
            });
            return this;
        }

        public PagamentoWebhookDTOBuilder ComApiVersion(string apiVersion)
        {
            CustomInstantiator(f => new PagamentoWebhookDTO
            {
                Id = f.Random.Long(1, 1000),
                Action = f.Lorem.Word(),
                ApiVersion = apiVersion,
                Data = new PagamentoWebhookDataDTO
                {
                    Id = f.Random.AlphaNumeric(10)
                },
                DateCreated = f.Date.Past(),
                LiveMode = f.Random.Bool(),
                Type = f.Lorem.Word(),
                UserId = f.Internet.UserName()
            });
            return this;
        }

        public PagamentoWebhookDTOBuilder ComData(PagamentoWebhookDataDTO data)
        {
            CustomInstantiator(f => new PagamentoWebhookDTO
            {
                Id = f.Random.Long(1, 1000),
                Action = f.Lorem.Word(),
                ApiVersion = f.Random.AlphaNumeric(5),
                Data = data,
                DateCreated = f.Date.Past(),
                LiveMode = f.Random.Bool(),
                Type = f.Lorem.Word(),
                UserId = f.Internet.UserName()
            });
            return this;
        }

        public PagamentoWebhookDTOBuilder ComDateCreated(DateTime dateCreated)
        {
            CustomInstantiator(f => new PagamentoWebhookDTO
            {
                Id = f.Random.Long(1, 1000),
                Action = f.Lorem.Word(),
                ApiVersion = f.Random.AlphaNumeric(5),
                Data = new PagamentoWebhookDataDTO
                {
                    Id = f.Random.AlphaNumeric(10)
                },
                DateCreated = dateCreated,
                LiveMode = f.Random.Bool(),
                Type = f.Lorem.Word(),
                UserId = f.Internet.UserName()
            });
            return this;
        }

        public PagamentoWebhookDTOBuilder ComLiveMode(bool liveMode)
        {
            CustomInstantiator(f => new PagamentoWebhookDTO
            {
                Id = f.Random.Long(1, 1000),
                Action = f.Lorem.Word(),
                ApiVersion = f.Random.AlphaNumeric(5),
                Data = new PagamentoWebhookDataDTO
                {
                    Id = f.Random.AlphaNumeric(10)
                },
                DateCreated = f.Date.Past(),
                LiveMode = liveMode,
                Type = f.Lorem.Word(),
                UserId = f.Internet.UserName()
            });
            return this;
        }

        public PagamentoWebhookDTOBuilder ComType(string type)
        {
            CustomInstantiator(f => new PagamentoWebhookDTO
            {
                Id = f.Random.Long(1, 1000),
                Action = f.Lorem.Word(),
                ApiVersion = f.Random.AlphaNumeric(5),
                Data = new PagamentoWebhookDataDTO
                {
                    Id = f.Random.AlphaNumeric(10)
                },
                DateCreated = f.Date.Past(),
                LiveMode = f.Random.Bool(),
                Type = type,
                UserId = f.Internet.UserName()
            });
            return this;
        }

        public PagamentoWebhookDTOBuilder ComUserId(string userId)
        {
            CustomInstantiator(f => new PagamentoWebhookDTO
            {
                Id = f.Random.Long(1, 1000),
                Action = f.Lorem.Word(),
                ApiVersion = f.Random.AlphaNumeric(5),
                Data = new PagamentoWebhookDataDTO
                {
                    Id = f.Random.AlphaNumeric(10)
                },
                DateCreated = f.Date.Past(),
                LiveMode = f.Random.Bool(),
                Type = f.Lorem.Word(),
                UserId = userId
            });
            return this;
        }

        public PagamentoWebhookDTO Build() => Generate();
    }
}
