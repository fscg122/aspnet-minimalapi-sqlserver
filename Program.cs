using BeepoApi;
using BeepoApi.Data;
using BeepoApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContactDbContext>(options =>
{
    var defaultConnection = Environment.GetEnvironmentVariable("DefaultConnection");
    options.UseSqlServer(defaultConnection ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."));
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthHandlerMiddleware();
app.UseAuthorization();

app.MapGet("api/contact", async (ContactDbContext dbContext) =>
{
    var contacts = await dbContext.Contact.ToListAsync();
    return Results.Ok(contacts);
});

app.MapGet("api/contact/{id}", async (int id, ContactDbContext dbContext) =>
{
    var contact = await dbContext.Contact.FindAsync(id);
    if (contact is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(contact);
});

app.MapDelete("api/contact/{id}", async (int id, ContactDbContext dbContext) =>
{
    var contact = await dbContext.Contact.FindAsync(id);
    if (contact is null)
    {
        return Results.NotFound();
    }

    dbContext.Contact.Remove(contact);
    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

app.MapPut("/api/contact/{id}", async (int id, Contact updatedContact, ContactDbContext dbContext) =>
{
    var existingContact = await dbContext.Contact.FindAsync(id);
    if (existingContact is null)
    {
        return Results.NotFound();
    }

    existingContact.FirstName = updatedContact.FirstName;
    existingContact.LastName = updatedContact.LastName;
    existingContact.CompanyName = updatedContact.CompanyName;
    existingContact.Email = updatedContact.Email;
    existingContact.PhoneNumber = updatedContact.PhoneNumber;

    await dbContext.SaveChangesAsync();

    return Results.Ok(existingContact);
});

app.MapPost("/api/contact", async (Contact newContact, ContactDbContext dbContext) =>
{
    dbContext.Contact.Add(newContact);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/api/contact/{newContact.Id}", newContact);
});

app.Run();
