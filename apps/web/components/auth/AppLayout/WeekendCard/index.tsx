import { Box } from "@chakra-ui/react"
import { MainContainer, DescriptionContainer } from "./styles"

interface WeekendCardProps {
  value: string
  description: string
}

const WeekendCard: React.FC<WeekendCardProps> = (props) => {
  const { value, description } = props
  return (
    <MainContainer>
      <Box
        alignItems="flex-end"
        display="flex"
        height="55%"
        justifyContent="center"
      >
        {/* <ValueContainer>{value}</ValueContainer> */}
        <Box
          color="#FFFFFF"
          fontFamily="Comfortaa"
          fontSize={
            value.length < 10 ? "34px" : value.length < 26 ? "28px" : "22px"
          }
          fontStyle="normal"
          fontWeight="700"
          lineHeight="32px"
          marginBottom={value.length < 10 ? "12px" : "0px"}
          textAlign="center"
          width="80%"
        >
          {value}
        </Box>
      </Box>
      <Box
        marginTop="5px"
        alignItems="center"
        display="flex"
        height="45%"
        justifyContent="center"
      >
        <DescriptionContainer>{description}</DescriptionContainer>
      </Box>
    </MainContainer>
  )
}

export default WeekendCard
