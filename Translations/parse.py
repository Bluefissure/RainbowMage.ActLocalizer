from xml.dom.minidom import parse
import json, re
import xml.dom.minidom
import codecs
from collections import defaultdict

def readfrom_xml(xml_file, languge, textdict=None):
    if not textdict:
        textdict = defaultdict(dict)
    DOMTree = xml.dom.minidom.parse(xml_file)
    collection = DOMTree.documentElement
    if collection.hasAttribute("Form"):
        print("Element Type:{}".format(collection.getAttribute("Form")))
        texts = collection.getElementsByTagName("Control")
        for text in texts:
            unique_key = "{}:{}".format(text.getAttribute("ControlPath"), text.getAttribute("UniqueName"))
            if text.hasAttribute("Text") and text.hasAttribute("UniqueName"):
                textdict[unique_key].update({
                        languge: text.getAttribute("Text"),
                    })
    elif collection.getElementsByTagName("TreeViewTranslationEntry"):
        entries = collection.getElementsByTagName("TreeViewTranslationEntry")
        for entry in entries:
            unique_key = "{}:{}".format("TreeViewTranslationEntry", entry.getAttribute("Original"))
            if entry.hasAttribute("Original"):
                textdict[unique_key].update({
                        languge: entry.firstChild.data,
                    })
    return textdict

def parse_file(filename):
    textdict_en = readfrom_xml("en-US/{}".format(filename), "en")
    textdict_cn = readfrom_xml("zh-CN/{}".format(filename), "cn", textdict_en)
    output_set = set()
    with codecs.open(filename.replace(".xml",".txt"), "w", "utf8") as f:
        for (_, value) in textdict_cn.items():
            if "cn" in value and "en" in value:
                en_str = value["en"].replace("\r","").replace("\n","\\n").strip()
                cn_str = value["cn"].replace("\r","").replace("\n","\\n").strip()
                if not en_str or not cn_str or en_str in output_set or not re.search(r"[\u4e00-\u9fa5]", cn_str):
                    continue
                f.write("{}|{}\n".format(en_str, cn_str))
                output_set.add(en_str)

if __name__=="__main__":
    for file in ["FormActMain.xml", "ConfigTreeView.xml"]:
        parse_file(file)
        print("Parsed {}".format(file))