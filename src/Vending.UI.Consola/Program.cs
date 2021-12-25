using System;
using System.Collections.Generic;
using Vending.UI.Consola;
using Vending;
using Vending.Modelos;
using Vending.Subsitemas;
using Vending.Subsitemas.Monetarios;

var parrillaGolosinas = new Producto[,]{
        {   new Golosina("KitKat", 1.10M), new Golosina("Chicles de fresa", .80M),
            new Golosina("Lacasitos", 1.50M), new Golosina("Palotes", .90M) },
        {   new Golosina("Kinder Bueno", 1.8M), new Golosina("Bolsa Haribo", 1),
            new Golosina("Chetoos", 1.20M), new Golosina("Twix", 1) },
        {   new Golosina("Maiz", 0.7M), new Golosina("M&M’S", 1.3M),
            new Golosina("Papa Delta", 1.20M), new Golosina("Chicles de menta", .80M) },
        {   new Golosina("Gusanitos", 1.5M), new Golosina("Crunch", 1.10M),
            new Golosina("Milkybar", 1.10M), new Golosina("Patatas fritas", 1.1M) },
        {   new Refresco("CocoCola", 1.5M, true), new ParaFarma("Gelocatil", 1.10M),
            new Refresco("Chus Limon", 1.10M), new ParaFarma("Paracetamol", 1.1M) },
        
        };

var dispensador = new Dispensador(parrillaGolosinas);
var ctrlPagos = new ControlDePagos();
var ctrlSeguridad = new SeguridadPin(new List<string> { "1234", "4567", "5678" });

var sistema = new MaquinaVending(dispensador, ctrlPagos, ctrlSeguridad);
var vista = new Vista();
var controlador = new Controlador(sistema, vista);

controlador.Run();