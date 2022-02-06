#define PINno

using Vending.UI.Consola;
using Vending;
using Vending.Subsitemas;
using Vending.Subsitemas.Economicos;
using Vending.Modelos;

var appConfig = new AppConfig();
var config = appConfig.Get();

// using Vending.Data;
// IRepoDispensador repoJson = new RepoDispensadorJson();
// var dispensador = new Dispensador(repoJson);

var matriz = new Producto[,]{
        {   new ParaFarma("Paracetamol", 2.5M), new Golosina("Surtido 50gr", 1.3M, 50),
            new RefrescoDietetico("KakoCola", 1.5M, 120) , new Golosina("Gominolas 50gr", .80M,50) },
        {   new RefrescoDietetico("CocoCola", 1.5M), new ParaFarma("Gelocatil", 1.10M),
            new Refresco("Chus Limon", 1.10M, 30), new ParaFarma("Dalsi", 1.1M) },
        {   new Golosina("Candy Crush", 1.5M), new Golosina("Caramelo", 1.10M),
            new Golosina("Gominolas 30gr", 1.10M, 30), new Golosina("Aceitunas", 1.1M) },
        {   new Golosina("Nube Algodón", 1.20M), new Refresco("Chus Naranja", 1.10M),
            new ParaFarma("Apiteral", 1.20M), new ParaFarma("Aspirina", 3.1M) },
        {   new RefrescoDietetico("AguaLoca", 1.5M, 30), new ParaFarma("Espidifen", 2.20M),
            new Refresco("Chus Kola", 1.10M), new Golosina("Chicle", 0.7M) },
        };
var dispensador = new Dispensador(matriz);

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




