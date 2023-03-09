import {
  AccordionItem,
  AccordionButton,
  AccordionPanel,
} from "@chakra-ui/react"
import {
  MobileDivider,
  MobileIconNotSelected,
  MobileIconSelected,
  MobileQuestionContent,
  MobileQuestionItemWrapper,
  MobileQuestionNumber,
  MobileQuestionTitle,
  MobileQuestionTitleWrapper,
} from "./styles"

const QuestionItem = () => {
  const isHovered = false
  return (
    <>
      <AccordionItem border={0}>
        <AccordionButton padding={0}>
          <MobileQuestionItemWrapper>
            <MobileQuestionTitleWrapper>
              <MobileQuestionNumber isHovered={isHovered}>
                01.
              </MobileQuestionNumber>
              <MobileQuestionTitle isHovered={isHovered}>
                How long does it take for a tattoo ?
              </MobileQuestionTitle>
            </MobileQuestionTitleWrapper>

            {isHovered ? <MobileIconSelected /> : <MobileIconNotSelected />}
          </MobileQuestionItemWrapper>
        </AccordionButton>
        <AccordionPanel padding={0}>
          <MobileQuestionContent isHovered={isHovered}>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim
            ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
            aliquip ex ea commodo consequat.
          </MobileQuestionContent>
        </AccordionPanel>
      </AccordionItem>

      <MobileDivider noMargin={false} />
    </>
  )
}

export default QuestionItem
