import { Accordion } from "@chakra-ui/react"
import QuestionItem from "./QuestionItem"
import { FAQsMobileSectionWrapper } from "./styles"

const FAQsMobileSection = () => (
  <FAQsMobileSectionWrapper>
    <Accordion allowMultiple defaultIndex={[0]}>
      <QuestionItem />
      <QuestionItem />
      <QuestionItem />
    </Accordion>
  </FAQsMobileSectionWrapper>
)

export default FAQsMobileSection
