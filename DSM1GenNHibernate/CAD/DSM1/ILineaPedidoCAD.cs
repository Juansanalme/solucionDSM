
using System;
using DSM1GenNHibernate.EN.DSM1;

namespace DSM1GenNHibernate.CAD.DSM1
{
public partial interface ILineaPedidoCAD
{
LineaPedidoEN ReadOIDDefault (int id
                              );

void ModifyDefault (LineaPedidoEN lineaPedido);



int New_ (LineaPedidoEN lineaPedido);

void Modify (LineaPedidoEN lineaPedido);


void Destroy (int id
              );


void Eliminar_producto (int p_LineaPedido_OID, int p_carrito_OID);

void Anyadir_producto (int p_LineaPedido_OID, int p_carrito_OID);

System.Collections.Generic.IList<LineaPedidoEN> Obtener_lineas (int first, int size);


void Quito_linea_a_carroYprecio (int id
                                 );


int Anyado_lineaYprecio (LineaPedidoEN lineaPedido);

System.Collections.Generic.IList<LineaPedidoEN> ReadAll (int first, int size);
}
}
