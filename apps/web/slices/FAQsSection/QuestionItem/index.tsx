import {
  // useEffect,
  useState,
} from "react"
import {
  IconNotSelected,
  QuestionContent,
  QuestionItemWrapper,
  QuestionNumber,
  QuestionTitle,
  IconSelected,
  Divider,
} from "./styles"

type QuestionItemProps = {
  id: number
  questionTitle: string
  questionDescription: string
}

const QuestionItem: React.FC<QuestionItemProps> = ({
  id,
  questionTitle,
  questionDescription,
}) => {
  // const isHovered = false
  // const isSelected = false

  console.log("id", id)
  console.log("questionTitle", questionTitle)
  console.log("questionDescription", questionDescription)

  const [isHovered, setHovered] = useState(false)
  const [isSelected, setSelected] = useState(false)

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

// useEffect(() => {
//   const question = document.getElementById("question")
//   question.addEventListener("mouseover", () => {
//     setHovered(true)
//     console.log("hovered")
//   })

//   question.addEventListener("mouseout", () => {
//     setHovered(false)
//   })

//   question.addEventListener("click", () => {
//     setSelected((isSelected) => !isSelected)
//     console.log("clicked")
//   })

//   return () => {
//     question.removeEventListener("mouseover", () => {
//       setHovered(true)
//       console.log("hovered")
//     })

//     question.removeEventListener("mouseout", () => {
//       setHovered(false)
//     })

//     question.removeEventListener("click", () => {
//       setSelected((isSelected) => !isSelected)
//       console.log("clicked")
//     })
//   }
// }, [])
