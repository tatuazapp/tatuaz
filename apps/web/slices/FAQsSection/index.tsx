import { useState } from "react"
import QuestionItem from "./QuestionItem"
import { FAQsSectionWrapper } from "./styles"

const FAQsSection = () => {
  const [selectedQuestion, setSelectedQuestion] = useState(0)

  return (
    <FAQsSectionWrapper>
      <QuestionItem />
      <QuestionItem />
      <QuestionItem />
    </FAQsSectionWrapper>
  )
}

export default FAQsSection
