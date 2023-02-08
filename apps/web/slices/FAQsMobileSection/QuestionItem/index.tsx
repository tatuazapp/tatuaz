import {
  AccordionItem,
  AccordionButton,
  AccordionPanel,
} from "@chakra-ui/react"
import {
  Divider,
  IconNotSelected,
  QuestionContent,
  QuestionItemWrapper,
  QuestionNumber,
  QuestionTitle,
  QuestionTitleWrapper,
} from "./styles"

const QuestionItem = () => {
  const isHovered = false
  const isSelected = false

  return (
    <>
      <AccordionItem border={0}>
        {/* <AccordionItem> */}
        <AccordionButton padding={0}>
          <QuestionItemWrapper>
            <QuestionTitleWrapper>
              <QuestionNumber isHovered={isHovered} isSelected={isSelected}>
                01.
              </QuestionNumber>
              <QuestionTitle isHovered={isHovered} isSelected={isSelected}>
                How long does it take for a tattoo ?
              </QuestionTitle>
            </QuestionTitleWrapper>

            <IconNotSelected />
          </QuestionItemWrapper>
        </AccordionButton>
        <AccordionPanel padding={0}>
          <QuestionContent isHovered={isHovered} isSelected={isSelected}>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim
            ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
            aliquip ex ea commodo consequat.
          </QuestionContent>
        </AccordionPanel>
      </AccordionItem>

      <Divider noMargin={false} />
    </>
  )
}

export default QuestionItem
