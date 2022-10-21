using System;
using System.Collections.Generic;
using tp1;
using tp2;

namespace tpfinal
{
	class Estrategia
	{
//		a medida que se va respondiendo y el sistema descarta lo falso,  imprime las posibles predicciones  que quedan en el arbol.
		public String Consulta1(ArbolBinario<DecisionData> arbol)
		{
			string resutl = "";
//			si el nodo es hoja, retorna la prediccion.
			if (arbol.esHoja()) {
				resutl = arbol.getDatoRaiz().ToString() + "\n";
				return resutl;
			}
// 			si no es hoja
			resutl += Consulta1(arbol.getHijoDerecho());// consulta al hijoDerecho, en busca de la prediccion/nodo-hoja
			resutl += Consulta1(arbol.getHijoIzquierdo());// consulta al hijoIzquierdo, en busca de la prediccion/nodo-hoja
			
			return resutl;
		}

//		se crea una lista para que los datos del los nodos persistan.Ademas se le pasa la posicion.
		public String Consulta2(ArbolBinario<DecisionData> arbol){
			List<string> lista = new List<string>();
			int len = 0;
			return Consulta2(arbol,lista,len);
		}
		
		
//		retorna todos los caminos hacia la prediccion.
		private String Consulta2(ArbolBinario<DecisionData> arbol, List<string> lista, int pos)
		{
			string resutl = "";
			
			lista.Insert(pos,arbol.getDatoRaiz().ToString());// se guarda el dato
			pos++;// se incrementa la posicion para el siguiente elemento
			
			if (arbol.esHoja()) {
//				se guardo los resultados de la lista hasta la posicion.
				for (int i = 0; i < pos; i++) {
					resutl += lista[i] + " | ";
				}
				
				return resutl;
			}
			
			resutl += Consulta2(arbol.getHijoIzquierdo(),lista,pos) + "\n";
			resutl += Consulta2(arbol.getHijoDerecho(),lista,pos)+ "\n";
			

			return resutl; // ahora si funca
		}
		public String Consulta3(ArbolBinario<DecisionData> arbol)
		{
			Cola<ArbolBinario<DecisionData>> c = new Cola<ArbolBinario<DecisionData>>();
			ArbolBinario<DecisionData> aux ;
			int contNiv = 0;
			bool paso = false; // bool para verificar que el lvl no se imprima duplicado
			c.encolar(arbol);
			c.encolar(null);
			
			string result = "";
			
			while (!c.esVacia()) {
				aux = c.desencolar();
					
				if (aux == null) {
					contNiv++;
					paso = false;
					
					if(!c.esVacia())
						c.encolar(null);
				}else{
					
					if (!paso) {
						result += "\nNivel: "+contNiv +"\n";
						paso = true;
					}
					
					result += aux.getDatoRaiz().ToString();
				
					if (aux.getHijoDerecho() != null) {
						c.encolar(aux.getHijoDerecho());
					}
					if (aux.getHijoIzquierdo() != null) {
						c.encolar(aux.getHijoIzquierdo());
					}
				}
			}
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
				
//				se agregan los hijos a la raiz.no llega hasta que se completen los subArboles,cuando esten completos se agregan
				arbol.agregarHijoDerecho(hijoDer);
				arbol.agregarHijoIzquierdo(hijoIzq);
				
				return arbol;
			}
			
		}
	
	}
}