import { down } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const GetATattooButtonWrapper = styled.div`
  display: flex;
  justify-content: center;
  margin-top: ${({ theme }) => theme.sizes.xxlarge};
  margin-bottom: ${({ theme }) => theme.sizes.xxxlarge};

  ${down("md")} {
    margin-top: ${({ theme }) => theme.sizes.large};
    margin-bottom: ${({ theme }) => theme.sizes.xlarge};
  }
`

export const ButtonContainer = styled.div`
  width: ${rem(200)};
`
