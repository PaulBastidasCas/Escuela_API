namespace Escuela.ApiTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            var rutaMaterias = "api/Materias";

            httpClient.BaseAddress = new Uri("https://localhost:7267/");
            //#### PRUEBA DE CRUD ####

            //Lectura de datos
            var response = httpClient.GetAsync("api/Materias").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var materias = Newtonsoft.Json.JsonConvert.DeserializeObject<Modelos.ApiResult<List<Modelos.Materia>>>(json);

            //Insercion de datos
            var nuevaMateria = new Modelos.Materia()
            {
                ID = 0,
                Nombre = "Estructura de Datos",
                NivelMateria = 3
            };

            //Invocar el serico web para insertar la nueva materia
            var materiaJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevaMateria);
            var content = new StringContent(materiaJson, System.Text.Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(rutaMaterias, content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            //Deserializar la respuesta
            var materiaCreada = Newtonsoft.Json.JsonConvert.DeserializeObject<Modelos.ApiResult<Modelos.Materia>>(json);

            //Actualizacion de datos
            materiaCreada.Data.Nombre = "Estructura";
            materiaJson = Newtonsoft.Json.JsonConvert.SerializeObject(materiaCreada.Data);
            content = new StringContent(materiaJson, System.Text.Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{rutaMaterias}/{materiaCreada.Data.ID}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            var materiaActualizada = Newtonsoft.Json.JsonConvert.DeserializeObject<Modelos.ApiResult<Modelos.Materia>>(json);

            //Eliminar datos
            response = httpClient.DeleteAsync($"{rutaMaterias}/{materiaCreada.Data.ID}").Result;
            json = response.Content.ReadAsStringAsync().Result;
            var especieEliminada = Newtonsoft.Json.JsonConvert.DeserializeObject<Modelos.ApiResult<Modelos.Materia>>(json);

            Console.WriteLine(json);
            Console.ReadLine();
        }
    }
}
