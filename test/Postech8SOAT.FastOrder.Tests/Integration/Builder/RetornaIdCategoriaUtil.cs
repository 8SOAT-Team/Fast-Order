namespace Postech8SOAT.FastOrder.Tests.Integration.Builder;
public class RetornaIdCategoriaUtil
{
    private static List<Guid> categoriasId = new List<Guid> { new Guid("0194d8c4-2d04-4172-a63a-4d381eadf729"),
    new Guid("07c470aa-606f-4792-849a-02433c121117"),new Guid("6224b6c0-26e9-42fa-8b04-dc0e9fd6b971"),new Guid("b553a212-9930-4e5a-a780-138a0a4a0b78")};
    public static Guid RetornaIdCategoria()
    {

        Random random = new Random();
        int randomIndex = random.Next(categoriasId.Count);
        Guid randomId = categoriasId[randomIndex];
        return randomId;
    }
}
