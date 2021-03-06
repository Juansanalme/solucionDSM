
/*PROTECTED REGION ID(CreateDB_imports) ENABLED START*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DSM1GenNHibernate.EN.DSM1;
using DSM1GenNHibernate.CEN.DSM1;
using DSM1GenNHibernate.CAD.DSM1;
using DSM1GenNHibernate.CP.DSM1;
using DSM1GenNHibernate.Utils;
using System.Collections;

/*PROTECTED REGION END*/
namespace InitializeDB
{
public class CreateDB
{
public static void Create (string databaseArg, string userArg, string passArg)
{
        String database = databaseArg;
        String user = userArg;
        String pass = passArg;

        // Conex DB
        SqlConnection cnn = new SqlConnection (@"Server=(local)\sqlexpress; database=master; integrated security=yes");

        // Order T-SQL create user
        String createUser = @"IF NOT EXISTS(SELECT name FROM master.dbo.syslogins WHERE name = '" + user + @"')
            BEGIN
                CREATE LOGIN ["                                                                                                                                     + user + @"] WITH PASSWORD=N'" + pass + @"', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
            END"                                                                                                                                                                                                                                                                                    ;

        //Order delete user if exist
        String deleteDataBase = @"if exists(select * from sys.databases where name = '" + database + "') DROP DATABASE [" + database + "]";
        //Order create databas
        string createBD = "CREATE DATABASE " + database;
        //Order associate user with database
        String associatedUser = @"USE [" + database + "];CREATE USER [" + user + "] FOR LOGIN [" + user + "];USE [" + database + "];EXEC sp_addrolemember N'db_owner', N'" + user + "'";
        SqlCommand cmd = null;

        try
        {
                // Open conex
                cnn.Open ();

                //Create user in SQLSERVER
                cmd = new SqlCommand (createUser, cnn);
                cmd.ExecuteNonQuery ();

                //DELETE database if exist
                cmd = new SqlCommand (deleteDataBase, cnn);
                cmd.ExecuteNonQuery ();

                //CREATE DB
                cmd = new SqlCommand (createBD, cnn);
                cmd.ExecuteNonQuery ();

                //Associate user with db
                cmd = new SqlCommand (associatedUser, cnn);
                cmd.ExecuteNonQuery ();

                System.Console.WriteLine ("DataBase create sucessfully..");
        }
        catch (Exception ex)
        {
                throw ex;
        }
        finally
        {
                if (cnn.State == ConnectionState.Open) {
                        cnn.Close ();
                }
        }
}

public static void InitializeData ()
{
        /*PROTECTED REGION ID(initializeDataMethod) ENABLED START*/
        try
        {
                // Insert the initilizations of entities using the CEN classes



                RegistradoCEN registradoCEN = new RegistradoCEN ();
                RegistradoCP registradoCP = new RegistradoCP ();
                CarritoCEN carritoCEN = new CarritoCEN ();
                CarritoCP carritoCP = new CarritoCP ();
                PedidoCEN pedidoCEN = new PedidoCEN ();

                Console.WriteLine (" ===============================================");
                Console.WriteLine (" =                                             =");
                Console.WriteLine (" =      ########    ########     ########      =");
                Console.WriteLine (" =         ##            ##         ##         =");
                Console.WriteLine (" =         ##          ##           ##         =");
                Console.WriteLine (" =         ##        ##             ##         =");
                Console.WriteLine (" =      ########    ########     ########      =");
                Console.WriteLine (" =                                             =");
                Console.WriteLine (" =             ONLINE SHOP - BETA              =");
                Console.WriteLine (" ===============================================");


                //CREAMOS USUARIOS REGISTRADOS Y SU CARRITOS
                int tester = registradoCP.Nuevo_usuarioYcarrito ("Beta", "Tester", 20, new DateTime (1997, 8, 6), "28595475X", "Test", "Beater", false).Id;
                int registrado0 = registradoCP.Nuevo_usuarioYcarrito ("Pablo", "Manez", 20, new DateTime (1997, 8, 6), "6984984X", "Test", "Pablomanez", true).Id;
                int registrado1 = registradoCP.Nuevo_usuarioYcarrito ("Kirito", "Kun", 21, new DateTime (1997, 5, 4), "25698568X", "asuna", "Kirito", false).Id;
                int registrado2 = registradoCP.Nuevo_usuarioYcarrito ("Dan", "Senpai", 20, new DateTime (1997, 8, 21), "23906238S", "easy", "Dan", false).Id;

                Console.WriteLine ("Inicializando datos...\n\n");

                //CREO TRES CATEGORIAS
                CategoriaCEN categoriaCEN = new CategoriaCEN ();

                Console.Write ("Creo tres categorías:");
                Console.WriteLine ("  -espada");
                Console.WriteLine ("  -pistola");
                Console.WriteLine ("  -arma");


                int[] categorias = new int [10];
                categorias [0] = categoriaCEN.New_ ("Espada", 0, "espada.jpg");
                categorias [1] = categoriaCEN.New_ ("Pistola", 0, "pistola.jpg");
                categorias [2] = categoriaCEN.New_ ("Escopeta", 0, "escopeta.jpg");
                categorias [3] = categoriaCEN.New_ ("Rifle", 0, "rifle.jpg");
                categorias [4] = categoriaCEN.New_ ("Zonas", 0, "zonas.jpg");
                categorias [5] = categoriaCEN.New_ ("Gemas", 0, "gemas.jpg");
                categorias [6] = categoriaCEN.New_ ("Miscelánea", 0, "miscelanea.jpg");
                categorias [7] = categoriaCEN.New_ ("Cuchillo", 0, "cuchillo.jpg");
                categorias [9] = categoriaCEN.New_ ("Arma", 0, "arma.jpg");

                categoriaCEN.Anyadir_supercat (categorias [0], categorias [9]);
                categoriaCEN.Anyadir_supercat (categorias [1], categorias [9]);
                categoriaCEN.Anyadir_supercat (categorias [2], categorias [9]);
                categoriaCEN.Anyadir_supercat (categorias [3], categorias [9]);
                categoriaCEN.Anyadir_supercat (categorias [4], categorias [6]);
                categoriaCEN.Anyadir_supercat (categorias [5], categorias [6]);

                //VIDEOJUEGOS
                VideojuegoCEN videojuegoCEN = new VideojuegoCEN ();
                int [] videojuegos = new int [20];
                videojuegos [0] = videojuegoCEN.New_ ("Left 4 Dead 2", "l4d2.jpg");
                videojuegos [1] = videojuegoCEN.New_ ("Blade & Soul", "byb.jpg");
                videojuegos [2] = videojuegoCEN.New_ ("World of Warcraft", "wow.jpg");
                videojuegos [3] = videojuegoCEN.New_ ("NieR: Automata", "nier.jpg");
                videojuegos [4] = videojuegoCEN.New_ ("Call of Duty", "cod.jpg");
                videojuegos [5] = videojuegoCEN.New_ ("El Rey Arturo: La leyenda de la espada", "arturo.jpg");
                videojuegos [6] = videojuegoCEN.New_ ("The legend of Zelda", "zelda.jpg");
                videojuegos [8] = videojuegoCEN.New_ ("Minecraft", "maincra.jpg");
                videojuegos [9] = videojuegoCEN.New_ ("Diablo III", "d3.jpg");
                videojuegos [10] = videojuegoCEN.New_ ("Counter Strike: Global Offensive", "csgo.jpg");
                videojuegos [11] = videojuegoCEN.New_ ("Dota", "dot.jpg");
                videojuegos [12] = videojuegoCEN.New_ ("Entropia Universe", "entropia.jpg");
                videojuegos [13] = videojuegoCEN.New_ ("Outlast", "outlast.jpg");
                videojuegos [14] = videojuegoCEN.New_ ("EVE Online", "eve.jpg");
                videojuegos [15] = videojuegoCEN.New_ ("Portal II", "portal2.jpg");
                videojuegos [16] = videojuegoCEN.New_ ("Deus Ex: Mankid Divided", "deus.jpg");
                videojuegos [17] = videojuegoCEN.New_ ("Half Life 3", "hl3.jpg");
                videojuegos [18] = videojuegoCEN.New_ ("Evolve Stage 2", "evolve.jpg");
                videojuegos [19] = videojuegoCEN.New_ ("Dead Space", "deadspace.jpg");


                //ARTICULOS
                ArticuloCEN articuloCEN = new ArticuloCEN ();
                int[] articulos = new int[40];
                articulos[0] = articuloCEN.New_("FrostMourne", 10.01, categorias [0], "Un arma muy especial", 5, "frostmourne.jpg", "", videojuegos [5]);
                articulos[1] = articuloCEN.New_("Escopeta Frost L4D", 1, categorias [2], "Piun, piun", 5, "frostshotgun.jpg", "", videojuegos [2]);
                articulos[2] = articuloCEN.New_("Contrato Virtuoso", 800, categorias [0], "Espada YohRa", 5, "nier.jpg", "94c9f72ffe6746fd8470177e07f96a35", videojuegos [3]);
                articulos[3] = articuloCEN.New_("Raygun", 50, categorias [4], "Arma de rayos molona", 5, "raygun.jpg", "", videojuegos [4]);
                articulos[4] = articuloCEN.New_("Excalibur", 90, categorias [5], "Espada del buen Rey Arturo", 5, "excalibur.jpg", "3b4ffcc403c2404d84a1a07cfc25e3c4", videojuegos [5]);
                articulos[5] = articuloCEN.New_("Daga del tiempo", 550, categorias [1], "Daga de Prince of Persia, puede controlar el tiempo", 5, "time.jpg", "", videojuegos [6]);
                articulos[6] = articuloCEN.New_("Braum Papá Noel", 10, categorias[1], "Skin navideña del señor Braum", 5, "braum.jpg", "", videojuegos[6]);
                articulos[7] = articuloCEN.New_("Shotgun", 320, categorias[2], "Escopeta Fallout, con animaciones incluidas", 5, "shotgun.jpg", "766c4bcf7f4047538ec032ba132c6750", videojuegos[6]);
                articulos[8] = articuloCEN.New_("Reinhardt", 550, categorias[1], "Martillo moón", 5, "hammer.jpg", "c68e42799e404507833685845e82d489", videojuegos[6]);
                articulos[9] = articuloCEN.New_("AWP Fenix", 550, categorias[1], "Daga de Prince of Persia, puede controlar el tiempo", 5, "phoenix.jpg", "", videojuegos[6]);
                articulos[10] = articuloCEN.New_("Elucidator", 390, categorias[1], "Espada del buen Kirito", 80, "elucidator.jpg", "aac6b699d9be46d99586d4e761310139", videojuegos[6]);
                articulos[11] = articuloCEN.New_("Ice Famas", 550, categorias[4], "Skin de hielo para esta molona arma", 5, "icefamas.jpg", "8b11a1145baf4665858c6abdaade764f", videojuegos[6]);
                articulos[12] = articuloCEN.New_("Anillo único", 999, categorias[1], "El anillo único", 5, "anillo.jpg", "314bdc71309d4c09949f9ab30ba92ac1", videojuegos[6]);
                articulos[13] = articuloCEN.New_("Skin Black Pearl", 52, categorias[1], "Cuchillo molón CS:GO", 5, "csgo.png", "", videojuegos[6]);
                articulos[14] = articuloCEN.New_("Raygun Mark II", 37, categorias[3], "Doble de potencia que su hermano pequeño", 5, "mark2.jpg", "", videojuegos[6]);
                articulos[15] = articuloCEN.New_("Kayn cazador de almas", 10, categorias[2], "Skin Kayn cazador de almas League of Legends", 5, "kayn.jpg", "", videojuegos[6]);
                articulos[16] = articuloCEN.New_("AK-47 Phoenix", 550, categorias[1], "Skin Phoenix para AK47", 5, "phoenixak.jpg", "1b35a7d44c0c4cdd885b4e2638d10b3b", videojuegos[6]);
                


                //ANYADO ARTICULOS A FAVORITO

                List<int> favoritos1 = new List<int>();
                favoritos1.Add (articulos[0]);
                List<int> favoritos2 = new List<int>();
                favoritos2.Add (articulos[2]);
                registradoCEN.Añadir_fav (registrado0, favoritos1);
                registradoCEN.Añadir_fav (registrado0, favoritos2);

                //CREO DOS PUJAS
                PujaCEN pujaCEN = new PujaCEN ();
                List<int> pujas = new List<int>();

                int pujaid = pujaCEN.New_ (DateTime.Now, 10, articulos[0], 10, -1, false, false);
                int pujaid2 = pujaCEN.New_ (DateTime.Now, 20, articulos[6], 20, -1, false, false);
                int pujaid3 = pujaCEN.New_(DateTime.Now, 15, articulos[2], 15, -1, false, false);
                int pujaid4 = pujaCEN.New_(DateTime.Now, 25, articulos[10], 25, -1, false, false);
                int pujaid5 = pujaCEN.New_(DateTime.Now, 30, articulos[15], 30, -1, false, false);
                int pujaid6 = pujaCEN.New_(DateTime.Now, 40, articulos[13], 40, -1, false, false);
                int pujaid7 = pujaCEN.New_(DateTime.Now, 32, articulos[2], 32, -1, false, false);

                pujas.Add (pujaid);
                pujas.Add (pujaid2);

                RegistradoEN max = null;
                PujaEN puja = pujaCEN.get_IPujaCAD ().ReadOIDDefault (pujaid);
                OfertaPujaCP ofertaPujaCP = null;


                List<int> carrito = new List<int>();
                LineaPedidoCEN lineaPedidoCEN = new LineaPedidoCEN ();
                LineaPedidoCP lineaPedidoCP = new LineaPedidoCP ();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine ("LOS USUARIOS CREADOS PREVIAMENTE REALIZAN DIVERSAS ACCIONES");
                //LLAMO AL CP NEW DE LINEA DE PEDIDO

                CarritoEN carritoEN = carritoCEN.get_ICarritoCAD ().ReadOIDDefault (registrado0);
                /*
                 * System.Console.WriteLine("Anyado 2 "+articuloCEN.get_IArticuloCAD().ReadOIDDefault(articulo1).Nombre+" al carrito de "+registradoCEN.get_IRegistradoCAD().ReadOIDDefault(registrado0).N_usuario);
                 * lineaPedidoCP.Anyado_lineaYprecio(20, articulo1, registrado0);
                 * System.Console.WriteLine("El precio del carrito es: "+carritoCEN.get_ICarritoCAD().ReadOIDDefault(registrado0).Precio + "\n");
                 */
                System.Console.WriteLine ("Anyado 2 " + articuloCEN.get_IArticuloCAD ().ReadOIDDefault (articulos[2]).Nombre + " al carrito de " + registradoCEN.get_IRegistradoCAD ().ReadOIDDefault (registrado0).N_usuario);
                lineaPedidoCP.Anyado_lineaYprecio (2, articulos[2], registrado0);
                System.Console.WriteLine ("El precio del carrito es: " + carritoCEN.get_ICarritoCAD ().ReadOIDDefault (registrado0).Precio + "\n");


                //CALCULO EL PRECIO DE UN CARRITO
                carritoCP = new CarritoCP ();
                Console.WriteLine ("Precio del carrito: " + carritoCEN.get_ICarritoCAD ().ReadOIDDefault (registrado0).Precio);
                carritoCP.Calcular_precio (registrado0);
                Console.WriteLine ("Precio del carrito al calcularlo: " + carritoCEN.get_ICarritoCAD ().ReadOIDDefault (registrado0).Precio);


                //CONSEGUIR TODAS LAS LINEAS DE UN CARRITO
                IList<LineaPedidoEN> lineas = lineaPedidoCEN.Obtener_lineas (0, 50);

                IList<int> lineasid = new List<int>();

                foreach (LineaPedidoEN linea in lineas) {
                        if (linea.Carrito.Id == registrado0) {
                                lineasid.Add (linea.Id);
                        }
                }

                //FINALIZO LA COMPRA
                carritoCP = new CarritoCP ();

                Console.WriteLine ("Finalizo la compra de pablomanez:");
                carritoCP.Finalizar_compra (registrado0, carritoCEN.get_ICarritoCAD ().ReadOIDDefault (registrado0).Precio);
                System.Console.WriteLine ("El precio del carrito es: " + carritoCEN.get_ICarritoCAD ().ReadOIDDefault (registrado0).Precio + "\n");

                Console.WriteLine ("Ahora " + articuloCEN.get_IArticuloCAD ().ReadOIDDefault (articulos[1]).Nombre + " tiene " + articuloCEN.get_IArticuloCAD ().ReadOIDDefault (articulos[1]).Stock + " unidades en stock \n");

                //CONSIGO TODOS LOS PEDIDOS DE UN USUARIO
                pedidoCEN = new PedidoCEN ();
                IList<PedidoEN> pedidos = pedidoCEN.Obtener_pedidos (0, 50);
                IList<int> histPedidos = new List<int>();

                foreach (PedidoEN pedido in pedidos) {
                        if (pedido.Registrado.Id == registrado0) {
                                histPedidos.Add (pedido.Id);
                        }
                }

                //PEDIDOS DE UN USUARIO
                Console.WriteLine ("Accedo al historial del usuario pablomanez:");
                foreach (int pedId in histPedidos) {
                        int i = 1;
                        PedidoEN ped = pedidoCEN.get_IPedidoCAD ().ReadOIDDefault (pedId);

                        Console.WriteLine ("///////////////////////// Pedido numero: " + i + " /////////////////////////");
                        Console.WriteLine ("Fecha: " + ped.Fecha);
                        Console.WriteLine ("Contenido: ");

                        IList<LineaPedidoEN> lineas2 = lineaPedidoCEN.Obtener_lineas (0, 50);
                        foreach (LineaPedidoEN linea2 in lineas2) {
                                if (linea2.Pedido.Id == pedId) {
                                        Console.WriteLine ("ID LINEA DE PEDIDO: " + linea2.Id);
                                        Console.WriteLine ("ARTICULO: " + articuloCEN.get_IArticuloCAD ().ReadOIDDefault (linea2.Articulo.Id).Nombre);
                                        Console.WriteLine ("CANTIDAD: " + linea2.Cantidad + "\n");
                                }
                        }
                }



                /*
                 *  //BUSCO ARTICULOS POR NOMBRE
                 *  System.Console.WriteLine ("USO: Busqueda_por_nombre()");
                 *  String ans = Console.ReadLine ();
                 *  IList<ArticuloEN> busq_nombre = articuloCEN.Busqueda_por_nombre (ans);
                 *  foreach (ArticuloEN art in busq_nombre) {
                 *          System.Console.WriteLine ("NOMBRE: " + art.Nombre);
                 *          System.Console.WriteLine ("PRECIO: " + art.Precio);
                 *  }
                 */

                //INICIAMOS LAS PUJAS
                try
                {
                        System.Console.WriteLine ("Puja#" + puja.Id + " | MAX: " + puja.Id_usuario + " " + puja.Puja_max + "$\n");
                        ofertaPujaCP = new OfertaPujaCP ();

                        System.Console.WriteLine ("Kirito-kun puja " + 15 + "$");
                        ofertaPujaCP.Nueva_oferta (DateTime.Now, DateTime.Now, registrado1, pujaid, 15); //Nueva oferta de Kirito-kun
                        puja = pujaCEN.get_IPujaCAD ().ReadOIDDefault (pujaid);                         //Actualizo puja
                        max = registradoCEN.get_IRegistradoCAD ().ReadOIDDefault (puja.Id_usuario);     //Actualizo nombre del usuario con la puja mas alta
                        System.Console.WriteLine ("Puja#" + puja.Id + " | MAX: " + max.N_usuario + " " + puja.Puja_max + "$\n");

                        System.Console.WriteLine ("DatrixZ puja " + 20 + "$");
                        ofertaPujaCP.Nueva_oferta (DateTime.Now, DateTime.Now, registrado2, pujaid, 20); //Nueva oferta de DatrixZ mayor que la anterior
                        puja = pujaCEN.get_IPujaCAD ().ReadOIDDefault (pujaid);
                        max = registradoCEN.get_IRegistradoCAD ().ReadOIDDefault (puja.Id_usuario);
                        System.Console.WriteLine ("Puja#" + puja.Id + " | MAX: " + max.N_usuario + " " + puja.Puja_max + "$\n");

                        System.Console.WriteLine ("Pablo-sensei puja " + 10 + "$");
                        ofertaPujaCP.Nueva_oferta (DateTime.Now, DateTime.Now, registrado0, pujaid, 10); //Nueva oferta de Pablo-sensei menor que la anterior
                        puja = pujaCEN.get_IPujaCAD ().ReadOIDDefault (pujaid);
                        max = registradoCEN.get_IRegistradoCAD ().ReadOIDDefault (puja.Id_usuario);
                        System.Console.WriteLine ("Puja#" + puja.Id + " | MAX: " + max.N_usuario + " " + puja.Puja_max + "$\n");
                }
                catch (Exception e) { System.Console.WriteLine (e.Message + "\n"); }

                try
                {
                        System.Console.WriteLine ("DatrixZ puja " + 50 + "$");
                        ofertaPujaCP.Nueva_oferta (DateTime.Now, DateTime.Now, registrado2, pujaid, 50); //Nueva oferta de DatrixZ contra s� mismo
                        puja = pujaCEN.get_IPujaCAD ().ReadOIDDefault (pujaid);
                        max = registradoCEN.get_IRegistradoCAD ().ReadOIDDefault (puja.Id_usuario);
                        System.Console.WriteLine ("Puja#" + puja.Id + " | MAX: " + max.N_usuario + " " + puja.Puja_max + "$\n");
                }
                catch (Exception e) { System.Console.WriteLine (e.Message + "\n"); }


                System.Console.WriteLine ("Puja#" + puja.Id + " | MAX: " + max.N_usuario + " " + puja.Puja_max + " " + puja.Cerrada + "$\n");

                PujaCP pujaCP = new PujaCP ();
                System.Console.WriteLine ("Cierro puja 1");
                /*
                 * pujaCP.Terminar_puja (pujaid, puja.Fecha, puja.Puja_inicial, puja.Puja_max, puja.Id_usuario, true);
                 * try
                 * {
                 *      System.Console.WriteLine ("Cierro puja 1 otra vez");
                 *      pujaCP.Terminar_puja (pujaid, puja.Fecha, puja.Puja_inicial, puja.Puja_max, puja.Id_usuario, true);
                 * }
                 * catch (Exception e) { System.Console.WriteLine (e.Message + "\n"); }
                 * try
                 * {
                 *      System.Console.WriteLine ("Cierro puja 2");
                 *      pujaCP.Terminar_puja (pujaid2, puja.Fecha, puja.Puja_inicial, puja.Puja_max, puja.Id_usuario, true);
                 * }
                 * catch (Exception e) { System.Console.WriteLine (e.Message + "\n"); }
                 *
                 * try
                 * {
                 *      System.Console.WriteLine ("Pablo-sensei puja " + 100 + "$");
                 *      ofertaPujaCP.Nueva_oferta (DateTime.Now, DateTime.Now, registrado1, pujaid, 100); //Nueva oferta de Pablo-sensei en una puja cerrada
                 *      puja = pujaCEN.get_IPujaCAD ().ReadOIDDefault (pujaid);
                 *      max = registradoCEN.get_IRegistradoCAD ().ReadOIDDefault (puja.Id_usuario);
                 *      System.Console.WriteLine ("Puja#" + puja.Id + " | MAX: " + max.N_usuario + " " + puja.Puja_max + "$\n");
                 * }
                 * catch (Exception e) { System.Console.WriteLine (e.Message + "\n"); }
                 */


                //CREO UNA VALORACION Y MODIFICO SU TEXTO
                ValoracionCEN valoracionCEN = new ValoracionCEN ();
                System.Console.WriteLine ("CREO UNA VALORACION Y LE CAMBIO EL TEXTO");
                int valoracionCEN_id1 = valoracionCEN.New_ (10, "La verdad es que es la hostia, pero quiero que Lujan me apruebe", registrado0, articulos[1]);

                System.Console.WriteLine (valoracionCEN.get_IValoracionCAD ().ReadOIDDefault (valoracionCEN_id1).Texto);
                int pMod = valoracionCEN.get_IValoracionCAD ().ReadOIDDefault (valoracionCEN_id1).Puntuacion;
                valoracionCEN.Modify (valoracionCEN_id1, pMod, "He cambiado el texto y si, quiero que Lujan me apruebe");
                System.Console.WriteLine (valoracionCEN.get_IValoracionCAD ().ReadOIDDefault (valoracionCEN_id1).Texto);


                lineaPedidoCP.Anyado_lineaYprecio (2, articulos[2], registrado0);
                lineaPedidoCP.Anyado_lineaYprecio (2, articulos[2], registrado1);

                // p.e. CustomerCEN customer = new CustomerCEN();
                // customer.New_ (p_user:"user", p_password:"1234");

                /*PROTECTED REGION END*/
        }
        catch (Exception ex)
        {
                System.Console.WriteLine (ex.InnerException);
                throw ex;
        }
}
}
}
