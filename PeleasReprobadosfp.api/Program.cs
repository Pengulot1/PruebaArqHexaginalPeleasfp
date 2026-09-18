using PeleasReprobadosfp.application.Application.Fighters;
using PeleasReprobadosfp.application.Application.Weapons;
using PeleasReprobadosfp.application.Application;
using PeleasReprobadosfp.domain.Domain.Interfaces;
using PeleasReprobadosfp.domain.Domain.Services;
using PeleasReprobadosfp.infrastructure.Infrastructure.Repositories;
using PeleasReprobadosfp.infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Registrar Servicios de Dominio
builder.Services.AddScoped<CombatDomainService>();

// Registrar Repositorios (Usa el de Firestore que creamos)
builder.Services.AddScoped<IFighterRepository, FirestoreFighterRepository>();
builder.Services.AddScoped<IWeaponRepository, FirestoreWeaponRepository>();

// Registrar Handlers de Fighter
builder.Services.AddScoped<CreateFighterCommandHandler>();
builder.Services.AddScoped<UpdateFighterCommandHandler>();
builder.Services.AddScoped<DeleteFighterCommandHandler>();
builder.Services.AddScoped<GetFighterByIdQueryHandler>();
builder.Services.AddScoped<GetAllFightersQueryHandler>();
builder.Services.AddScoped<EquipWeaponCommandHandler>();

// Registrar Handlers de Weapon
builder.Services.AddScoped<CreateWeaponCommandHandler>();
builder.Services.AddScoped<UpdateWeaponCommandHandler>();
builder.Services.AddScoped<DeleteWeaponCommandHandler>();
builder.Services.AddScoped<GetAllWeaponsQueryHandler>();
builder.Services.AddScoped<GetWeaponByIdQueryHandler>();

// Registrar Handler de Combate
builder.Services.AddScoped<SimulateCombatCommandHandler>();

// 1. A�ADIMOS ESTO: Servicios para tu API y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Preparamos la ruta al archivo JSON
string credentialPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebase-key.json");
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

// 2. Creamos la conexi�n �nica a Firestore usando tu ID de proyecto
builder.Services.AddSingleton(s => Google.Cloud.Firestore.FirestoreDb.Create("juegopeleashexagonal"));

// --- FIN CONFIGURACI�N FIREBASE ---

// Esta l�nea es el l�mite: todo lo que usa "builder" debe ir antes de esto
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // 2. A�ADIMOS ESTO: Activar la interfaz de Swagger cuando est�s programando
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{

}
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 3. CAMBIAMOS ESTO: En lugar de MapRazorPages, usamos MapControllers
app.MapControllers();

app.Run();
