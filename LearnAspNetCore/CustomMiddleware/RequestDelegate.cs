using System.Threading.Tasks;


namespace CustomMiddleware
{
    public delegate Task RequestDelegate(Context context);
}