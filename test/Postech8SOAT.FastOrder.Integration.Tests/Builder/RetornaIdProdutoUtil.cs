namespace Postech8SOAT.FastOrder.Integration.Tests.Builder;
public class RetornaIdProdutoUtil
{
    private static List<Guid> produtosId = new List<Guid> { new Guid("0e05db30-b5ec-4e26-b79e-a43b64743ab5"),
    new Guid("f0fdbefb-08b2-4ad7-bdfc-8c3f0e070d8e")};
    public static Guid RetornaIdProduto()
    {

        Random random = new Random();
        int randomIndex = random.Next(produtosId.Count);
        Guid randomId = produtosId[randomIndex];
        return randomId;
    }
}
