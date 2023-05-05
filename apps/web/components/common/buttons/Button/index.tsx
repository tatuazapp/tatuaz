import {
  ButtonProps as ChakraButtonProps,
  Button as ChakraButton,
} from "@chakra-ui/react"
import { ArrowRight, ArrowUpRight } from "@styled-icons/bootstrap"
import { FunctionComponent } from "react"

type ButtonKind = "arrowUpRight" | "arrowRight" | "primary"

type ButtonProps = {
  kind?: ButtonKind
} & ChakraButtonProps

const kinds = {
  arrowUpRight: {
    color: "black",
    colorScheme: "primary",
    rightIcon: <ArrowUpRight size={24} />,
    size: "lg",
    width: { base: "100%" },
  },
  arrowRight: {
    color: "black",
    colorScheme: "primary",
    rightIcon: <ArrowRight size={24} />,
    size: "lg",
  },
  primary: {
    color: "black",
    colorScheme: "primary",
    size: "lg",
  },
} satisfies Record<string, ChakraButtonProps>

const buildProps = ({ kind }: { kind?: ButtonKind }) => kinds[kind ?? "primary"]

const Button: FunctionComponent<ButtonProps> = ({ kind, ...chakraProps }) => {
  const props = buildProps({ kind })

  return <ChakraButton {...props} {...chakraProps} />
}

export default Button
