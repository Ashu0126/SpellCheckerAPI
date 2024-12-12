using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Words;
using NHunspell;
using SpellCheckerAPI.Models;

namespace SpellCheckerAPI.Services
{
    public class SpellCheckerService
    {
        public List<SpellError> ProcessWordDocument(string filePath, Hunspell spellChecker)
        {
            var errors = new List<SpellError>();

            try
            {
                // Load the document
                var doc = new Document(filePath);

                // Extract all paragraphs (each line in the document is a paragraph)
                var paragraphs = doc.GetChildNodes(Aspose.Words.NodeType.Paragraph, true);

                int lineNumber = 1;

                foreach (Aspose.Words.Paragraph paragraph in paragraphs)
                {
                    // Split text into words
                    var words = paragraph.GetText().Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                    int wordPosition = 0; // Position within the line

                    foreach (var word in words)
                    {
                        if (!spellChecker.Spell(word))
                        {
                            var suggestions = spellChecker.Suggest(word);

                            // Record the error with line number and position
                            errors.Add(new SpellError
                            {
                                Word = word,
                                LineNumber = lineNumber,
                                Position = wordPosition,
                                Suggestions = suggestions
                            });
                        }

                        wordPosition++;
                    }

                    lineNumber++; // Move to the next line
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {filePath}: {ex.Message}");
            }

            return errors;
        }
    }
}