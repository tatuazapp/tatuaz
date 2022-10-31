import {
  Box,
  Center,
  Container,
  SkeletonCircle,
  SkeletonText,
  Stat,
  StatArrow,
  StatGroup,
  StatHelpText,
  StatLabel,
  StatNumber,
} from "@chakra-ui/react"
import AppLayout from "../components/auth/AppLayout"

const Index = () => (
  <AppLayout>
    <Center h="400px">
      <StatGroup color="white">
        <Stat>
          <StatLabel>Sent</StatLabel>
          <StatNumber>345,670</StatNumber>
          <StatHelpText>
            <StatArrow type="increase" />
            23.36%
          </StatHelpText>
        </Stat>

        <Stat>
          <StatLabel>Clicked</StatLabel>
          <StatNumber>45</StatNumber>
          <StatHelpText>
            <StatArrow type="decrease" />
            9.05%
          </StatHelpText>
        </Stat>
      </StatGroup>
    </Center>
    <Center>
      <Container maxW="550px">
        <Box boxShadow="lg" padding="6">
          <SkeletonCircle size="10" />
          <SkeletonText mt="4" noOfLines={4} spacing="4" />
        </Box>
      </Container>
    </Center>
  </AppLayout>
)

export default Index
