/*
    Programmeurs:   BOUSSAOUT Mustapha
                    ZOGONA Jonathan
                    TANYA Karelle

    Date:           29 Oct. 2019
 
    Solution:       TextEditiorKMJ.sln
    Projet:		    TextEditiorKMJ.csproj
    Classe:         TextEditorKMJParentForm.cs
 
    Buts:           Devoir 2 - Phase F
                    Afficher les polices installées, Enregistrer des objets d'une zone de liste déroulante
                    Accéder et travailler avec l'objet de la classe ToolStripCombox.
                    Changer la police et la taille du texte sélectionnée
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ce = TextEditorKMJ.TextEditorKMJGenerale.CEMessages;

namespace TextEditorKMJ
{
    class TextEditorKMJGenerale
    {

        #region Messages d'erreurs

        public static string[] tableauMessages = new string[16];

        public enum CEMessages
        {
            creationTextEditorKMJ,
            erreurIndeterminee,
            erreurOutOfMemory,
            erreurFileNotFound,
            erreurArgumentException,
            erreurArgumentNullExeption,
            erreurExternalException,
            erreurMauvaiseExtension,
            enregistrerErreur,
            ouvrirErreur,
            enregistrementErreur,
            selectionChangedErreur,
            editionErreur,
            changerStyleErreur,
            changerAlignementErreur,
            InvalidEnumArgumentErreur
        }

        public static void InitMessages()
        {
            tableauMessages[(int)ce.creationTextEditorKMJ] = "Il est impossible de créer un document TextEditorKMJ.";
            tableauMessages[(int)ce.erreurIndeterminee] = "Erreur indéterminée...";
            tableauMessages[(int)ce.enregistrerErreur] = "Ce document n'a pas été enregistré.\nvoulez vous l'enregistrer maintenant?";
            tableauMessages[(int)ce.ouvrirErreur] = "le systeme ne permet pas d'ouvrir ce fichier.\nconsulter le manuel pour plus de details.";
            tableauMessages[(int)ce.enregistrementErreur] = "le systeme eprouve des difficulté a enregistrer ce fichier.\nconsulter le manuel pour plus de details.";
            tableauMessages[(int)ce.erreurOutOfMemory] = "Il n'y a pas assez de mémoire pour continuer ou le format du document est invalide.";
            tableauMessages[(int)ce.erreurFileNotFound] = "Le document spécifié n'existe pas.";
            tableauMessages[(int)ce.erreurArgumentException] = "Le nom du document n'est pas valide.";
            tableauMessages[(int)ce.erreurArgumentNullExeption] = "Le nom du document est null.";
            tableauMessages[(int)ce.erreurExternalException] = "Le document a été sauvegardée sous un mauvais format.";
            tableauMessages[(int)ce.erreurMauvaiseExtension] = "Mauvaise extension de document.";
            tableauMessages[(int)ce.selectionChangedErreur] = "Une erreur est survenue lors de l'actualisation du texte.\nConsulter le manuel pour plus de details. ";
            tableauMessages[(int)ce.editionErreur] = "Une erreur est survenue lors de l'édition du texte.\nConsulter le manuel pour plus de details. ";
            tableauMessages[(int)ce.changerStyleErreur] = "Une erreur est survenue lors du changement de style.\nConsulter le manuel pour plus de details. ";
            tableauMessages[(int)ce.changerAlignementErreur] = "Une erreur est survenue lors du changement de l'alignement.\nConsulter le manuel pour plus de details. ";
            tableauMessages[(int)ce.InvalidEnumArgumentErreur] = "format ne spécifie pas un membre valide de TextDataFormat..\nConsulter le manuel pour plus de details. ";
        }

        #endregion

        #region Méthode public pour Enlever les crochets

        public static void EnleverCrochet(ToolStripMenuItem oMenu)
        {
            if (oMenu != null)
            {
                foreach (ToolStripItem item in oMenu.DropDownItems)
                {
                    if (item is ToolStripMenuItem)
                        (item as ToolStripMenuItem).Checked = false;
                }
            }
        }

        #endregion

    }
}