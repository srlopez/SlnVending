using System;
using System.IO;
using Newtonsoft.Json;


namespace Vending.Data
{
    using Vending.Modelos;

    public class RepoDispensadorJson : IRepoDispensador
    {
        private string _dataPath;
        private string _file;

        void IRepoDispensador.Inicializar()
        {
            // TODO: Parametizar 
            _dataPath = "../../data/";
            _file = _dataPath + "Dispensador.json";
        }
        void IRepoDispensador.Guardar(Producto[,] parrilla)
        {
            string json = JsonConvert.SerializeObject(parrilla, Formatting.Indented);
            File.WriteAllText(_file, json);
        }
        Producto[,] IRepoDispensador.Cargar()
        {
            var txtJson = File.ReadAllText(_file);
            dynamic dataJson = JsonConvert.DeserializeObject(txtJson);
            var filas = dataJson.Count;
            var columnas = dataJson[0].Count;

            var parrilla = new Producto[filas, columnas];
            for (var f = 0; f < filas; f++)
                for (var c = 0; c < columnas; c++)
                    parrilla[f, c] = toProducto(dataJson[f][c]);

            return parrilla;

            Producto toProducto(dynamic data)
            {
                string typeName = "Vending.Modelos." + data.Tipo + ", Vending.App.Modelos";
                var jsonProducto = JsonConvert.SerializeObject(data);
                Type type = Type.GetType(typeName);
                return JsonConvert.DeserializeObject(jsonProducto, type);
            }
        }
        /*
        return = new Producto[,]{
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
        */
    }
}



