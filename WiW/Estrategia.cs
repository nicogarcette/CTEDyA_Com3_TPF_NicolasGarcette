using System;
using System.Collections.Generic;
using tp1;
using tp2;

namespace tpfinal
{
	class Estrategia
	{
//		imprime las posibles predicciones que quedan hasta el momento en el arbol.
		public String Consulta1(ArbolBinario<DecisionData> arbol)
		{
			string resutl = "";
//			si es hoja, retorna la prediccion.
			if (arbol.esHoja()) {
				return arbol.getDatoRaiz() + "\n";
			}
			
// 			si no es hoja, se busca en los hijos
			if (arbol.getHijoIzquierdo() != null) {
				resutl += Consulta1(arbol.getHijoIzquierdo());// consulta al hijoIzquierdo.
			}
			if (arbol.getHijoDerecho() != null) {	
				resutl += Consulta1(arbol.getHijoDerecho());// consulta al hijoDerecho.
			}

			return resutl;
		}

		public String Consulta2(ArbolBinario<DecisionData> arbol){
			List<string> lista = new List<string>(); //se agrega un list para que el camino almacenado persista.
			return Consulta2(arbol,lista);
		}
//		retorna todos los caminos hacia la prediccion.
		private String Consulta2(ArbolBinario<DecisionData> arbol, List<string> lista){
			
			string resutl = "";
			
			lista.Add(arbol.getDatoRaiz().ToString());// se guarda el dato
//			si es hoja, devuelve el camino almacenado
			if (arbol.esHoja()) {
				foreach (var ele in lista) {
					resutl += ele + " | ";
				}
				return resutl;
			}
			
			if (arbol.getHijoIzquierdo() != null) {
				resutl += Consulta2(arbol.getHijoIzquierdo(),lista) + "\n";	
			}
			lista.RemoveAt(lista.Count -1);
			if (arbol.getHijoDerecho() != null) {
				resutl += Consulta2(arbol.getHijoDerecho(),lista)+ "\n";
			}
			
			return resutl;
		}
//		Imprime las decisiones y predicciones ordenadas segun nivel en el que se encuentran.
		public String Consulta3(ArbolBinario<DecisionData> arbol){
			
			Cola<ArbolBinario<DecisionData>> c = new Cola<ArbolBinario<DecisionData>>();
			ArbolBinario<DecisionData> aux ;
			int contNiv = 0;
			c.encolar(arbol);//se encola el arbol
			c.encolar(null);//se encola el separador para cuantificar el lvl.
			string result = "\nNivel: "+contNiv +"\n";
			
			while (!c.esVacia()) {
				aux = c.desencolar();
					
				if (aux == null) {
					contNiv++;
					if(!c.esVacia()){
						c.encolar(null);//si aun quedan elementos, encola separador.
						result += "\nNivel: "+contNiv +"\n";
					}
				}else{
					result += aux.getDatoRaiz().ToString();
					
					if (aux.getHijoDerecho() != null) 
						c.encolar(aux.getHijoDerecho());
					
					if (aux.getHijoIzquierdo() != null) 
						c.encolar(aux.getHijoIzquierdo());
				}
			}
			return result;
		}
		
//		desicionData toma como parametro Pregunta o Prediccion del clasificador.   
		public ArbolBinario<DecisionData> CrearArbol(Clasificador clasificador){
			
			ArbolBinario <DecisionData> arbol;

//			si es nodo-hoja se obtiene la prediccion y se retorna el nodo.
			if (clasificador.crearHoja()) {
				// con la prediccion se crea el nodo-hoja.
				arbol = new ArbolBinario<DecisionData>(new DecisionData(clasificador.obtenerDatoHoja()));
				return arbol;
			}else {
//				se obtiene la pregunta y se convierte en nodo-desicion
				arbol = new ArbolBinario<DecisionData>(new DecisionData(clasificador.obtenerPregunta()));
				
//				se obtiene el ClasificadorIzquierdo
				Clasificador izquierdo = clasificador.obtenerClasificadorIzquierdo();
//				se llama a crearArbol para que retorne el arbol del nodo izq
				ArbolBinario<DecisionData> hijoIzq = this.CrearArbol(izquierdo);
				
//				se obtiene el ClasificadorDerecho
				Clasificador derecho = clasificador.obtenerClasificadorDerecho();
//				se llama a crearArbol para que retorne el arbol del nodo izq
				ArbolBinario<DecisionData> hijoDer = this.CrearArbol(derecho);
				
//				se agregan los hijos a su nodoPadre.
				arbol.agregarHijoDerecho(hijoDer);
				arbol.agregarHijoIzquierdo(hijoIzq);
				
				return arbol;
			}
		}
	}
}