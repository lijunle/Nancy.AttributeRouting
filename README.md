# Nancy.AttributeRouting

[![Build status](https://ci.appveyor.com/api/projects/status/86jwerymo30f9949/branch/master?svg=true)](https://ci.appveyor.com/project/lijunle/nancy-attributerouting/branch/master)
[![Coverage Status](https://coveralls.io/repos/lijunle/Nancy.AttributeRouting/badge.svg?branch=master&service=github)](https://coveralls.io/github/lijunle/Nancy.AttributeRouting?branch=master)

Enable attribute routing for Nancy project, and build route URL with compiled-time checked lambda expression.

# Install

```powershell
PM> Install-Package Nancy.AttributeRouting
```

# Why to use AttributeRouting?

With AttributeRouting, there is no need to write [NancyModule](https://github.com/NancyFx/Nancy/wiki/Exploring-the-nancy-module), [route](https://github.com/NancyFx/Nancy/wiki/Defining-routes) and [content negotiate](https://github.com/NancyFx/Nancy/wiki/Content-Negotiation) are handled by attributes.

With AttributeRouting, you can write a clean MVVM (Model - View - View Model) approach with Nancy framework.

# Tutorial

This tutorial walks through how to use AttributeRouting to build pages. For Full examples, please check [examples](https://github.com/lijunle/Nancy.AttributeRouting/tree/master/Nancy.AttributeRouting.Examples) folder.

## Todo List Page

First, we are going to create a page to list todo items in the `/todo` routing.

The following are the corresponding view and view model. (*Note:* you can use any view engine, I use the default [Super Simple View Engine](https://github.com/NancyFx/Nancy/wiki/The-Super-Simple-View-Engine) in my example.)

```html
<!-- show todo list view, place as `/Views/TodoList.html` -->
<html>
    <body>
        <ul>
        @Each.Items
            <li>@Current</li>
        @EndEach
        </ul>
    </body>
</html>
```

```csharp
// show todo list view model, place as `/ViewModels/TodoList.cs`
public class TodoList
{
    private readonly ITodoDatabase database;

    public TodoList(ITodoDatabase database) // Use IoC to inject dependencies to view model
    {
        this.database = database;
    }

    public IEnumerable<string> Items => this.database.TodoItems;

    [Get("/todo")]
    [View(nameof(TodoList))]
    public TodoList Get() => this;
}
```

Notice the `Get` and `View` attribute on `Todo` class methods:

- The `Get` attribute register `/todo` path to Nancy routing table.
- The `View` attribute tells Nancy content negotiate which view template should pick up when browser requests HTML.
- The `View` attribute value (`nameof(TodoList)`) works with [Nancy view location convention](https://github.com/NancyFx/Nancy/wiki/View-location-conventions) to find out the proper view template to render. That is `/Views/TodoList.html` in my example.

## Add Todo Item Page

Next, we are going to create a page to add new Todo items. The new page is under route path `/todo/add`.

```html
<!-- add todo item view, place as `/Views/TodoAdd.html` -->
<html>
    <body>
        <form method="post">
            <input type="text" name="description" placeholder="Description..." />
            <input type="submit" value="Submit" />
        </form>
    </body>
</html>
```

```csharp
// add todo item view model, place as `/ViewModels/TodoAdd.cs`
[RoutePrefix("todo")]
public class TodoAdd
{
    private readonly ITodoDatabase database;

    public TodoAdd(ITodoDatabase database)
    {
        this.database = database;
    }

    [Get("add")]
    [View(nameof(TodoAdd))]
    public TodoAdd Get() => this;

    [Post("add")]
    public Response Post(Form form)
    {
        this.database.TodoItems.Add(form.Description);
        return new RedirectResponse("/todo");
    }

    public class Form
    {
        public string Description { get; set; }
    }
}
```

- This page shows a form to add a new todo item.
- The `RoutePrefix` attribute prefixes routing every route in the class
- The `Get` attribute on the `Get` method is to response `TodoAdd` view to user.
- The `Post` attribute on the `Post` method is to handle the HTTP POST method request.
- The `Post` method parameter binds the request payload as a non-primitive class (`Form` class here).
- The `Post` method returns `Nancy.Response` instance directly, so it will not do content negotiate.

## Generate Page URL in runtime

The `/todo` is hard coded in both pages. With project grows up, it is hard to update all route strings even do simple refactor.

`Nancy.AttributeRouting` provides an interface `IUrlBuilder` to generate page URL in runtime.

For example, update the redirect URL code in `TodoAdd` view model:

```csharp
// update TodoAdd view model in `/ViewModels/TodoAdd.cs`
public class TodoAdd
{
    private readonly IUrlBuilder builder; // injected from constructor

    public TodoAdd(IUrlBuilder builder, ITodoDatabase database)
    {
        // other codes keep the same...
        this.builder = builder;
    }

    public Response Post(Form form)
    {
        // other codes keep the same...
        string todoListUrl = this.builder.GetUrl<TodoList>(m => m.Get(null));
        return new RedirectResponse(todoListUrl);
    }
}
```

- The `IUrlBuilder` interface is registered to Nancy IoC at bootstrap. It is out-of-box to inject to view models.
- The `IUrlBuilder` interface provides `GetUrl<TViewModel>` method to generated view model URL in runtime.
- The `GetUrl` lambda expression arguments will replace route variables when necessary.

## Conclusion

We created two pages with Nancy and AttributeRouting. Following MVVM pattern, only models (the ITodoDatabase part), views and view models are created. In the view - view model part, we focus on how the view looks like, what functions we need to provide for the corresponding views.

Check [examples](https://github.com/lijunle/Nancy.AttributeRouting/tree/master/Nancy.AttributeRouting.Examples) folder to see a full MVVM approach to build website.

# API

See [Nancy.AttributeRouting.md](https://github.com/lijunle/Nancy.AttributeRouting/blob/master/Nancy.AttributeRouting/Nancy.AttributeRouting.md) file.

# License

MIT License.
