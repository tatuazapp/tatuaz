import { Facebook } from "@styled-icons/boxicons-logos/Facebook"
import { Instagram } from "@styled-icons/boxicons-logos/Instagram"
import { Twitter } from "@styled-icons/boxicons-logos/Twitter"
import { Youtube } from "@styled-icons/boxicons-logos/Youtube"
import { down } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../../../styles/utils"

export const FooterWrapper = styled.div`
  display: flex;
  align-items: center;

  min-height: ${rem(100)};
  padding-top: ${({ theme }) => theme.space.xlarge};
  padding-right: ${({ theme }) => theme.space.xxxxlarge};
  padding-bottom: ${({ theme }) => theme.space.xlarge};
  padding-left: ${({ theme }) => theme.space.xxxxlarge};

  background-color: ${({ theme }) => theme.colors.background2};

  ${down("md")} {
    padding-right: ${({ theme }) => theme.space.xlarge};
    padding-left: ${({ theme }) => theme.space.xlarge};
  }

  ${down("sm")} {
    flex-direction: column;
    justify-content: center;
    padding-top: ${({ theme }) => theme.space.xxxlarge};
    padding-bottom: ${({ theme }) => theme.space.xxxlarge};
  }
`

export const LogoWrapper = styled.div`
  width: 33%;
  font-size: ${rem(32)};
  font-weight: 600;
  color: ${({ theme }) => theme.colors.secondary};

  ${down("sm")} {
    width: 100%;
    padding-bottom: ${({ theme }) => theme.space.xxsmall};
    text-align: center;
  }
`

export const GreenWrapper = styled.span`
  color: ${({ theme }) => theme.colors.primary};
`

export const CopyrightWrapper = styled.div`
  display: flex;
  justify-content: center;

  width: 33%;

  font-size: ${({ theme }) => theme.space.medium};
  color: ${({ theme }) => theme.colors.secondary};

  ${down("sm")} {
    order: 3;
    width: 100%;
    text-align: center;
  }
`

export const SocialMediaWrapper = styled.div`
  display: flex;
  align-items: center;
  justify-content: end;
  width: 33%;

  ${down("sm")} {
    justify-content: center;
    padding-bottom: ${({ theme }) => theme.space.xxsmall};
  }
`

export const TwitterIcon = styled(Twitter)`
  height: ${({ theme }) => theme.space.xlarge};
  color: ${({ theme }) => theme.colors.primary};

  cursor: pointer;
  ${down("sm")} {
    height: ${({ theme }) => theme.space.xxlarge};
  }
`
export const YoutubeIcon = styled(Youtube)`
  height: ${({ theme }) => theme.space.xlarge};
  color: ${({ theme }) => theme.colors.primary};

  cursor: pointer;

  ${down("sm")} {
    height: ${({ theme }) => theme.space.xxlarge};
  }
`
export const InstagramIcon = styled(Instagram)`
  height: ${({ theme }) => theme.space.xlarge};
  color: ${({ theme }) => theme.colors.primary};

  cursor: pointer;
  ${down("sm")} {
    height: ${({ theme }) => theme.space.xxlarge};
  }
`
export const FacebookIcon = styled(Facebook)`
  height: ${({ theme }) => theme.space.large};
  color: ${({ theme }) => theme.colors.primary};

  cursor: pointer;
  ${down("sm")} {
    height: ${({ theme }) => theme.space.xlarge};
  }
`
