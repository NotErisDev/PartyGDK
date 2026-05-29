import Button from "@/components/inputs/Button.vue";
import Checkbox from "@/components/inputs/Checkbox.vue";
import Image from "@/components/inputs/Image.vue";
import Label from "@/components/inputs/Label.vue";
import Range from "@/components/inputs/Range.vue";
import TextInput from "@/components/inputs/TextInput.vue";

const registry = {
  button: Button,
  checkbox: Checkbox,
  image: Image,
  label: Label,
  range: Range,
  text: TextInput
};

export function registerInput(type, component) {
  registry[type] = component;
}

export function getInput(type) {
  return registry[type];
}