
using System;
using DSM1GenNHibernate.EN.DSM1;

namespace DSM1GenNHibernate.CAD.DSM1
{
public partial interface ICategoriaCAD
{
CategoriaEN ReadOIDDefault (int id
                            );

void ModifyDefault (CategoriaEN categoria);



int New_ (CategoriaEN categoria);

void Modify (CategoriaEN categoria);


void Destroy (int id
              );


void Anyadir_supercat (int p_Categoria_OID, int p_supercategoria_OID);

System.Collections.Generic.IList<CategoriaEN> ReadAll (int first, int size);


void Quitar_supercat (int p_Categoria_OID, int p_supercategoria_OID);

void Quitar_subcat (int p_Categoria_OID, System.Collections.Generic.IList<int> p_subcategoria_OIDs);
}
}
