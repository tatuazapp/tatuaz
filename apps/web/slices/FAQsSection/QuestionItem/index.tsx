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

const QuestionItem = () => {
  // const isHovered = false
  // const isSelected = false

  const [isHovered, setHovered] = useState(false)
  const [isSelected, setSelected] = useState(false)

  return (
    <div id="question">
      <QuestionItemWrapper>
        <QuestionNumber isHovered={isHovered} isSelected={isSelected}>
          01.
        </QuestionNumber>
        <QuestionTitle isHovered={isHovered} isSelected={isSelected}>
          Does Tattooing Hurt ?
        </QuestionTitle>
        <QuestionContent isHovered={isHovered} isSelected={isSelected}>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua.
        </QuestionContent>
        {isHovered || isSelected ? <IconSelected /> : <IconNotSelected />}
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
