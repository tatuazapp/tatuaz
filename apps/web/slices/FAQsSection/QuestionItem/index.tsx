import {
  IconNotSelected,
  QuestionContent,
  QuestionItemWrapper,
  QuestionNumber,
  QuestionTitle,
  IconSelected,
  Divider,
} from "./styles"

const QuestionItem = () => {
  const isHovered = false

  return (
    <div id="question">
      <QuestionItemWrapper>
        <QuestionNumber>01.</QuestionNumber>
        <QuestionTitle>Does Tattooing Hurt ?</QuestionTitle>
        <QuestionContent>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua.
        </QuestionContent>
        {isHovered ? <IconSelected /> : <IconNotSelected />}
      </QuestionItemWrapper>
      <Divider />
    </div>
  )
}

export default QuestionItem
