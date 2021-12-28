#define PINno

using Vending.UI.Consola;
using Vending;
using Vending.Subsitemas;
using Vending.Subsitemas.Monetarios;
using Vending.Data;

var appConfig = new AppConfig();
var config= appConfig.Get();

IRepoDispensador repoJson = new RepoDispensadorJson();
var dispensador = new Dispensador(repoJson);

var ctrlPagos = new ControlDePagos();
#if PIN
var ctrlSeguridad = new SeguridadPin(new List<string> { "1234", "4567", "5678" });
#else
var ctrlSeguridad = new SeguridadValor((int)config.SeguridadValor);
#endif

var sistema = new MaquinaVending(dispensador, ctrlPagos, ctrlSeguridad);
var vista = new Vista();
var controlador = new Controlador(sistema, vista);

controlador.Run();