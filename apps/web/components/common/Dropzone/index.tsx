import { Center, useColorModeValue, Icon } from "@chakra-ui/react"
import { FileEarmarkArrowUp } from "@styled-icons/bootstrap"
import { FunctionComponent, useCallback } from "react"
import { useDropzone } from "react-dropzone"
import { FormattedMessage } from "react-intl"

type DropzoneProps = {
  onFileAccepted: (file: File) => void
}

const Dropzone: FunctionComponent<DropzoneProps> = ({ onFileAccepted }) => {
  const onDrop = useCallback(
    (acceptedFiles: File[]) => {
      onFileAccepted(acceptedFiles[0])
    },
    [onFileAccepted]
  )

  const { getRootProps, getInputProps, isDragActive } = useDropzone({
    onDrop,
    accept: {
      "image/*": [".jpg", ".jpeg", ".png"],
    },
    maxFiles: 1,
    multiple: false,
  })

  const activeBg = useColorModeValue("gray.100", "gray.600")
  const borderColor = useColorModeValue(
    isDragActive ? "teal.300" : "gray.300",
    isDragActive ? "teal.500" : "gray.500"
  )

  return (
    <Center
      _hover={{ bg: activeBg }}
      bg={isDragActive ? activeBg : "transparent"}
      border="3px dashed"
      borderColor={borderColor}
      borderRadius={4}
      cursor="pointer"
      p={10}
      transition="background-color 0.2s ease"
      {...getRootProps()}
    >
      <input {...getInputProps()} />
      <Icon as={FileEarmarkArrowUp} mr={2} />
      <p>
        <FormattedMessage
          defaultMessage="Przeciągnij i upuść plik lub kliknij, aby wybrać"
          id="ikOoAh"
        />
      </p>
    </Center>
  )
}

export default Dropzone
