﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DSM1GenNHibernate.EN.DSM1;
using DSM1GenNHibernate.CEN.DSM1;
using DSM1GenNHibernate.CAD.DSM1;
using DSM1GenNHibernate.CP.DSM1;
using WebDSM.Models;
using WebMatrix.WebData;

namespace WebDSM.Controllers
{
    public class PedidoController : BasicController
    {
        // GET: Pedido
        public ActionResult Index()
        {
            SessionInitialize();

            PedidoCAD pedidoCAD = new PedidoCAD(session);
            PedidoCEN pedidoCEN = new PedidoCEN(pedidoCAD);

            IList<PedidoEN> pedidosEN = pedidoCEN.ReadAll(0, -1);
            IEnumerable<Pedido> peds = new AssemblerPedido().ConvertListENToModel(pedidosEN).ToList();

            SessionClose();

            return View(peds);
        }

        // GET: Pedido/Details/5
        public ActionResult Details(int id)
        {
            SessionInitialize();

            PedidoCAD cad = new PedidoCAD(session);
            PedidoCEN cen = new PedidoCEN(cad);

            PedidoEN en = cen.get_IPedidoCAD().ReadOIDDefault(id);
            PedidoYLineas model = new AssemblerPedido().ConvertENToViewModelUI(en);
            SessionClose();

            return View(model);
        }

        // GET: Pedido/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pedido/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedido/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pedido/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedido/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pedido/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult LoadPedidos()
        {
            if (Session["login"] != null)
            {
                int id = (int)Session["idusuario"];

                SessionInitialize();

                PedidoCAD cad = new PedidoCAD(session);
                PedidoCEN cen = new PedidoCEN(cad);

                IList<PedidoEN> listEN = cen.get_IPedidoCAD().ReadAll(0, -1);

                IList<PedidoEN> pedidosList = new List<PedidoEN>();
                foreach (PedidoEN pedido in listEN)
                {
                    if (pedido.Registrado.Id == id)
                    {
                        pedidosList.Add(pedido);
                    }
                }

                //Session["nCarrito"] = 0;
                IEnumerable<Pedido> enumPed = new AssemblerPedido().ConvertListENToModel(pedidosList);
                SessionClose();

                if (pedidosList.Count() == 0)
                {
                    return View("Index", null);
                }
                else
                {
                    return View("Index", enumPed);
                }
            }
            else
            {
                return RedirectToAction("../Home");
            }
	    }
    }
}
