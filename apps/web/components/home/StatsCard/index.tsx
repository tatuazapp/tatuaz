import { FunctionComponent } from "react"
import {
  MainContainer,
  DescriptionContainer,
  TopContainer,
  BottomContainer,
  ValueContainer,
} from "./styles"

type WeekendCardProps = {
  value: string
  description: string
}

const WeekendCard: FunctionComponent<WeekendCardProps> = ({
  value,
  description,
}) => (
  <MainContainer>
    <TopContainer>
      <ValueContainer length={value.length}>{value}</ValueContainer>
    </TopContainer>
    <BottomContainer>
      <DescriptionContainer>{description}</DescriptionContainer>
    </BottomContainer>
  </MainContainer>
)

export default WeekendCard
