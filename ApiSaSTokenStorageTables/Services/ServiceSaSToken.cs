using Azure.Data.Tables;
using Azure.Data.Tables.Sas;

namespace ApiSaSTokenStorageTables.Services
{
    public class ServiceSaSToken
    {
        //esta clase genera tpkens a partir de la 
        //tabla amlumnos
        private TableClient tablaAlumnos;

        public ServiceSaSToken(IConfiguration configuration)
        {
            string azureKeys = configuration.GetValue<string>("AzureKeys:StorageAccount");
            TableServiceClient serviceClient = new TableServiceClient(azureKeys);
            this.tablaAlumnos = serviceClient.GetTableClient("alumnos");

        }

        //queremos un token que solamente nos devuelva los alumnos
        //de un determinado curso

        public string GenerateSaSToken(string curso)
        {
            //necesitamos permisos de acceso
            TableSasPermissions permisos = TableSasPermissions.Read;
            //debemos crear un constructor con los permisos 
            //y el tiempo de acceso a la tabla
            TableSasBuilder builder = this.tablaAlumnos.GetSasBuilder(permisos, DateTime.Now.AddMinutes(50));
            //queremos que solamente nos muestre los cursos
            //(partiton key)
            builder.PartitionKeyStart = curso;
            builder.PartitionKeyEnd = curso;
            //con todo esto montado, nos dara una uri
            //de acceso
            Uri uriToken = this.tablaAlumnos.GenerateSasUri(builder);
            //extraemos la ruta HTTPS con el token
            string token = uriToken.AbsolutePath;
            return token;
        }
    }
}
