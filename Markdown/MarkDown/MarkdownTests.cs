using System;
using Xunit;


namespace PlanlyTask.MarkDown
{
    public class MarkdownTests
    {
        [Fact]
        public void Parses_normal_text_as_a_paragraph()
        {
            var markdown = "This will be a paragraph";
            var expected = "<p>This will be a paragraph</p>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void Parsing_italics()
        {
            var markdown = "_This will be italic_";
            var expected = "<p><em>This will be italic</em></p>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void Parsing_bold_text()
        {
            var markdown = "__This will be bold__";
            var expected = "<p><strong>This will be bold</strong></p>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void Mixed_normal_italics_and_bold_text()
        {
            var markdown = "This will be __mixed__";
            var expected = "<p>This will <em>be</em> <strong>mixed</strong></p>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void With_h1_header_level()
        {
            var markdown = "# This will be an h1";
            var expected = "<h1>This will be an h1</h1>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void With_h2_header_level()
        {
            var markdown = "## This will be an h2";
            var expected = "<h2>This will be an h2</h2>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void With_h6_header_level()
        {
            var markdown = "###### This will be an h6";
            var expected = "<h6>This will be an h6</h6>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void Unordered_lists()
        {
            var markdown = " Item 1\n Item 2";
            var expected = "<ul><li>Item 1</li><li>Item 2</li></ul>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void With_a_little_bit_of_everything()
        {
            var markdown = "# Header!\n __Bold Item__\n _Italic Item_";
            var expected = "<h1>Header!</h1><ul><li><strong>Bold Item</strong></li><li><em>Italic Item</em></li></ul>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void With_markdown_symbols_in_the_header_text_that_should_not_be_interpreted()
        {
            var markdown = "# This is a header with # and * in the text";
            var expected = "<h1>This is a header with # and * in the text</h1>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void With_markdown_symbols_in_the_list_item_text_that_should_not_be_interpreted()
        {
            var markdown = " Item 1 with a # in the text\n Item 2 with * in the text";
            var expected = "<ul><li>Item 1 with a # in the text</li><li>Item 2 with * in the text</li></ul>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void With_markdown_symbols_in_the_paragraph_text_that_should_not_be_interpreted()
        {
            var markdown = "This is a paragraph with # and * in the text";
            var expected = "<p>This is a paragraph with # and * in the text</p>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void Empty_lines_are_handled_correctly()
        {
            var markdown = "Line 1\n\nLine 2";
            var expected = "<p>Line 1</p><p>Line 2</p>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void Long_paragraph_is_processed_correctly()
        {
            var markdown = new string('a', 1000) + "\n" + new string('b', 1000);
            var expected = $"<p>{new string('a', 1000)}</p><p>{new string('b', 1000)}</p>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void Multi_line_list_items()
        {
            var markdown = " Item 1\n  Line 2 of item 1\n Item 2";
            var expected = "<ul><li>Item 1<br>Line 2 of item 1</li><li>Item 2</li></ul>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }

        [Fact]
        public void Invalid_markdown_does_not_fail()
        {
            var markdown = "# Header\n List item\nSome text\n Another item";
            var expected = "<h1>Header</h1><ul><li>List item</li></ul><p>Some text</p><ul><li>Another item</li></ul>";
            Assert.Equal(expected, Markdown.Parse(markdown));
        }
    }
}
