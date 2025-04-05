namespace AspNetCorePills.Web;

class MyService
{
    private static int value = 0;

    public MyService()
    {
        value++;
    }

    public int GetValue() => value;
}