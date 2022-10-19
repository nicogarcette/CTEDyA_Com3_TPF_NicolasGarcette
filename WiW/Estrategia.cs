using System;
using System.Collections.Generic;
using tp1;
using tp2;

namespace tpfinal
{
	class Estrategia
	{
		public String Consulta1(ArbolBinario<DecisionData> arbol)
		{
			string resutl = "Implementar consulta1";
			return resutl;
		}


		public String Consulta2(ArbolBinario<DecisionData> arbol)
		{
			string resutl = "Implementar consulta2";
			return resutl;
		}
		
		public String Consulta3(ArbolBinario<DecisionData> arbol)
		{
			string result = "Implementar consulta3";
			return result;
		}
		
		
		
//		el clasificador le pasa como parametro a desicionData  Pregunta o Prediccion.
		public ArbolBinario<DecisionData> CrearArbol(Clasificador clasificador)
		{
			DecisionData nodo;
			ArbolBinario <DecisionData> arbol;
			
//			si es nodo-hoja se obtiene la prediccion. se retorna el nodo.
			if (clasificador.crearHoja()) {
				nodo = new DecisionData(clasificador.obtenerDatoHoja());// con la prediccion se crea el nodo-hoja<DesicionData>
				arbol = new ArbolBinario<DecisionData>(nodo);// con el nodo se crea el arbol.
				return arbol;
			}else {
//				se obtiene la pregunta
				nodo = new DecisionData(clasificador.obtenerPregunta());
//				se convierte en nodo-desicion
				arbol = new ArbolBinario<DecisionData>(nodo);
				
//				se obtiene el ClasificadorIzquierdo
				Clasificador izquierdo = clasificador.obtenerClasificadorIzquierdo();
//				se llama a crearArbol para que retorne el arbol del nodo izq
				ArbolBinario<DecisionData> hijoIzq = this.CrearArbol(izquierdo);
				
//				se obtiene el ClasificadorIzquierdo
				Clasificador derecho = clasificador.obtenerClasificadorDerecho();
//				se llama a crearArbol para que retorne el arbol del nodo izq
				ArbolBinario<DecisionData> hijoDer = this.CrearArbol(derecho);
				
//				se agregan los hijos a la raiz.
				arbol.agregarHijoDerecho(hijoDer);
				arbol.agregarHijoIzquierdo(hijoIzq);
				
				return arbol;
			}
			
		}
	
	}
}