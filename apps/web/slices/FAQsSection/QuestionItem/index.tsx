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
        <QuestionNumber isHovered={isHovered}>01.</QuestionNumber>
        <QuestionTitle isHovered={isHovered}>
          Does Tattooing Hurt ?
        </QuestionTitle>
        <QuestionContent isHovered={isHovered}>
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
