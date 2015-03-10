# Nancy.AttributeRouting

Enable attribute routing for Nancy project, and build route URL with compiled-time checked lambda expression.

# Install

```powershell
PM> Install-Package Nancy.AttributeRouting
```

# Usage

```csharp
// attribute routing
public class MyViewModel
{
    [Get("/my")]
    public MyViewModel()
    {
    }

    [Get("/my/{property}")]
    [View("ViewModel")]
    public MyViewModel(string property)
    {
    }

    [Get("/my/result")]
    [Post("/my/result")]
    public object GetResult()
    {
        return new { Result = "MyResult" };
    }

    [Get("/my/redirect/to/{url}")]
    [Put("/my/redirect/to/{url}")]
    public Response RedirectTo(string url, IResponseFormatter response)
    {
        return response.AsRedirect(url);
    }
}
```

```csharp
// build route URL with compiled-time checked lambda expression
public void UrlBuilder_GetUrl(TinyIoCContainer container)
{
    IUrlBuilder builder = container.Resolve<TinyIoCContainer>();
    builder.GetUrl<MyViewModel>(() => new MyViewModel()); // => /my
    builder.GetUrl<MyViewModel>(() => new MyViewModel("constructor-value")); // => /my/constructor-value
    builder.GetUrl<MyViewModel>(v => v.GetResult()); // => /my/result
    builder.GetUrl<MyViewModel>(v => v.RedirectTo("redirect-url", null)); // => /my/redirect/to/redirect-url
}
```

# License

MIT License.
