using Laboratorio4.Controllers;
using Laboratorio4.Handlers;
using Laboratorio4.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Web.Mvc;

namespace UnitTestLab7.Mocks
{
    [TestClass]
    public class PlanetasMock
    {
        [TestMethod]
        public void AgregarMultiplesPlanetasVariosUsuarios()
        {
            ThreadStart usuario1 = new ThreadStart(SimulacionUsuarioCreandoPlanetas);
            ThreadStart usuario2 = new ThreadStart(SimulacionUsuarioCreandoPlanetas);
            Thread hilo1 = new Thread(usuario1);
            Thread hilo2 = new Thread(usuario2);
            hilo1.Start();
            hilo2.Start();
            hilo1.Join();
            hilo2.Join();
        }
        public void SimulacionUsuarioCreandoPlanetas()
        {
            //Arrange
            MyTestPostedFileBase archivoTest = new MyTestPostedFileBase(new System.IO.MemoryStream(), "/test.file", "TestFile");
            PlanetaModel planeta = new PlanetaModel
            {
                nombre = "Test-planeta",
                numeroAnillos = 100,
                archivo = archivoTest,
                tipo = "De prueba"
            };
            PlanetasHandler db = new PlanetasHandler();
            bool exitoAlCrear = false;
            for (int intento = 0; intento < 10; ++intento)
            {
                //Act
                exitoAlCrear = db.crearPlaneta(planeta);
                //Assert
                Assert.IsTrue(exitoAlCrear);
            }
        }

        [TestMethod]
        public void CrearPlanetaMuestraMensajeCorrectoAlFallar()
        {
            //Arrange
            var mock = new Mock<PlanetasHandler>();
            PlanetaModel planeta = new PlanetaModel();
            planeta.nombre = "Testelia";
            mock.Setup(mocked => mocked.crearPlaneta(planeta)).Returns(false);

            //Act
            PlanetasController planetasController = new PlanetasController(mock.Object);
            ViewResult vista = planetasController.crearPlaneta(planeta) as ViewResult;

            //Assert
            Assert.AreEqual("Algo salio mal y no fue posible crear el planeta unu", vista.ViewBag.Message);
        }
    }
}
