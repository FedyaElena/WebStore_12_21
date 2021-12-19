using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace WebStore.Infrastructure.Middleware;

public class TestMiddleware
{
    private readonly RequestDelegate _Next;
    public TestMiddleware(RequestDelegate Next)
    {
        _Next = Next;
    }

    public async Task Invoke(HttpContext Context)
    {
        var processning_task = _Next(Context);

        await processning_task;
        //await _Next(Context); 
    }
}
