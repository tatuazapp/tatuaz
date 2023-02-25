import { Input } from "@chakra-ui/react"
import styled from "styled-components"

export const UsernameWrapper = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;

  margin-top: ${({ theme }) => theme.space.xxlarge};
`

export const InputWrapper = styled(Input)`
  max-width: 400px;
`

export const InputWithErrorWrapper = styled.div`
  width: 100%;
  max-width: 400px;
`
